#include "windows.h"

#include <d3d11.h>
#include <d3dx11.h>
#include <d3dx10.h>

#pragma comment (lib, "d3d11.lib")
#pragma comment (lib, "d3dx11.lib")
#pragma comment (lib, "d3dx10.lib")

#include <stdio.h>

HINSTANCE  appInstance;
HWND       parentWnd;
HWND	   d3dWnd;
int		   d3dWidth;
int		   d3dHeight;

LRESULT CALLBACK DLLWindowProc (HWND, UINT, WPARAM, LPARAM);

struct VERTEX{FLOAT X, Y, Z; D3DXCOLOR Color;};

void StartUpD3D(HWND hWnd);    
void InitGraphics(void);    
void InitPipeline(void);    
void RenderFrame(void);     
void ShutdownD3D(void);        

IDXGISwapChain *swapchain;             
ID3D11Device *dev;                     
ID3D11DeviceContext *devcon;           
ID3D11RenderTargetView *backbuffer;    
ID3D11InputLayout *pLayout;            
ID3D11VertexShader *pVertShader;
ID3D11PixelShader *pPixelShader;                
ID3D11Buffer *pVBuffer;                

BOOL RegisterDLLWindowClass(wchar_t szClassName[])
{
	WNDCLASSEX wc;
	wc.hInstance =  appInstance;
	wc.lpszClassName = (LPCWSTR)L"D3DWindowClass";
	wc.lpszClassName = (LPCWSTR)szClassName;
	wc.lpfnWndProc = DLLWindowProc;
	wc.style = CS_DBLCLKS;
	wc.cbSize = sizeof (WNDCLASSEX);
	wc.hIcon = LoadIcon (NULL, IDI_APPLICATION);
	wc.hIconSm = LoadIcon (NULL, IDI_APPLICATION);
	wc.hCursor = LoadCursor (NULL, IDC_ARROW);
	wc.lpszMenuName = NULL;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH) COLOR_BACKGROUND;
	if (!RegisterClassEx (&wc))
		return 0;
}

//The new thread
DWORD WINAPI ThreadProc( LPVOID lpParam )
{
	MSG messages;

	RegisterDLLWindowClass(L"D3DWindowClass");

	d3dWnd = CreateWindowEx (0, 
		L"D3DWindowClass", 
		L"D3DWindow",
		WS_CHILD, 
		0, 
		0, 
		d3dWidth, d3dHeight, 
		parentWnd, 
		NULL,
		appInstance, 
		NULL );

	ShowWindow (d3dWnd, SW_SHOWNORMAL);

	StartUpD3D(d3dWnd);

	while (GetMessage (&messages, NULL, 0, 0))
	{
		TranslateMessage(&messages);
		DispatchMessage(&messages);
	}

	ShutdownD3D();

	return 1;
}

//Our new windows proc
LRESULT CALLBACK DLLWindowProc (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_DESTROY:
		PostQuitMessage (0);
		break;

	case WM_PAINT:
		RenderFrame();
		break;

	case WM_SIZE:
		if (swapchain)
		{
			devcon->OMSetRenderTargets(0, 0, 0);
			backbuffer->Release();

			HRESULT hr;
			hr = swapchain->ResizeBuffers(0, 0, 0, DXGI_FORMAT_UNKNOWN, 0);

			ID3D11Texture2D* pBuffer;
			hr = swapchain->GetBuffer(0, __uuidof( ID3D11Texture2D), (void**) &pBuffer );

			hr = dev->CreateRenderTargetView(pBuffer, NULL,
				&backbuffer);
			pBuffer->Release();

			devcon->OMSetRenderTargets(1, &backbuffer, NULL );

			int width = LOWORD(lParam);
			int height = HIWORD(lParam);

			D3D11_VIEWPORT viewport;
			viewport.Width = width;
			viewport.Height = height;
			viewport.MinDepth = 0.0f;
			viewport.MaxDepth = 1.0f;
			viewport.TopLeftX = 0;
			viewport.TopLeftY = 0;
			devcon->RSSetViewports( 1, &viewport );
		}   
		break;

	default:
		return DefWindowProc (hwnd, message, wParam, lParam);
	}
	return 0;
}

//-----------------------------------------------------------------------------

void StartUpD3D(HWND hWnd)
{
	DXGI_SWAP_CHAIN_DESC scd;
	ZeroMemory(&scd, sizeof(DXGI_SWAP_CHAIN_DESC));

	scd.BufferCount = 1;                                    
	scd.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;     
	scd.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;      
	scd.OutputWindow = hWnd;                                
	scd.SampleDesc.Count = 1;                               
	scd.Windowed = TRUE;									

	D3D_FEATURE_LEVEL  FeatureLevelsSupported;

	HRESULT result = D3D11CreateDeviceAndSwapChain(NULL,
		D3D_DRIVER_TYPE_HARDWARE,
		NULL,
		0,
		NULL,
		NULL,
		D3D11_SDK_VERSION,
		&scd,
		&swapchain,
		&dev,
		&FeatureLevelsSupported,
		&devcon);

	ID3D11Texture2D *pBackBuffer;
	swapchain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);

	dev->CreateRenderTargetView(pBackBuffer, NULL, &backbuffer);
	pBackBuffer->Release();

	devcon->OMSetRenderTargets(1, &backbuffer, NULL);

	D3D11_VIEWPORT viewport;
	ZeroMemory(&viewport, sizeof(D3D11_VIEWPORT));

	viewport.TopLeftX = 0;
	viewport.TopLeftY = 0;
	viewport.Width = d3dWidth;
	viewport.Height = d3dHeight;

	devcon->RSSetViewports(1, &viewport);

	InitPipeline();
	InitGraphics();
}

void InitGraphics()
{
	VERTEX theTriangle[] =
	{
		{0.0f, 0.5f, 0.0f, D3DXCOLOR(1.0f, 0.0f, 0.0f, 1.0f)},
		{0.45f, -0.5, 0.0f, D3DXCOLOR(0.0f, 1.0f, 0.0f, 1.0f)},
		{-0.45f, -0.5f, 0.0f, D3DXCOLOR(0.0f, 0.0f, 1.0f, 1.0f)}
	};

	D3D11_BUFFER_DESC bd;
	ZeroMemory(&bd, sizeof(bd));

	bd.Usage = D3D11_USAGE_DYNAMIC;                
	bd.ByteWidth = sizeof(VERTEX) * 3;             
	bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;       
	bd.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;    

	dev->CreateBuffer(&bd, NULL, &pVBuffer);       

	D3D11_MAPPED_SUBRESOURCE ms;
	devcon->Map(pVBuffer, NULL, D3D11_MAP_WRITE_DISCARD, NULL, &ms);    
	memcpy(ms.pData, theTriangle, sizeof(theTriangle));                 
	devcon->Unmap(pVBuffer, NULL);                                      
}

void InitPipeline()
{
	ID3D10Blob *VS, *PS;
	HRESULT result = D3DX11CompileFromFile(L"shaders.shader", 0, 0, "VShader", "vs_4_0", 0, 0, 0, &VS, 0, 0);
	result = D3DX11CompileFromFile(L"shaders.shader", 0, 0, "PShader", "ps_4_0", 0, 0, 0, &PS, 0, 0);

	dev->CreateVertexShader(VS->GetBufferPointer(), VS->GetBufferSize(), NULL, &pVertShader);
	dev->CreatePixelShader(PS->GetBufferPointer(), PS->GetBufferSize(), NULL, &pPixelShader);

	devcon->VSSetShader(pVertShader, 0, 0);
	devcon->PSSetShader(pPixelShader, 0, 0);

	D3D11_INPUT_ELEMENT_DESC ied[] =
	{
		{"POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0},
		{"COLOR", 0, DXGI_FORMAT_R32G32B32A32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0},
	};

	dev->CreateInputLayout(ied, 2, VS->GetBufferPointer(), VS->GetBufferSize(), &pLayout);
	devcon->IASetInputLayout(pLayout);
}

void RenderFrame(void)
{
	devcon->ClearRenderTargetView(backbuffer, D3DXCOLOR(0.5f, 0.5f, 0.5f, 1.0f));

	UINT stride = sizeof(VERTEX);
	UINT offset = 0;
	devcon->IASetVertexBuffers(0, 1, &pVBuffer, &stride, &offset);
	devcon->IASetPrimitiveTopology(D3D10_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

	devcon->Draw(3, 0);

	swapchain->Present(0, 0);
}

void ShutdownD3D(void)
{
	pLayout->Release();
	pVertShader->Release();
	pPixelShader->Release();
	pVBuffer->Release();
	swapchain->Release();
	backbuffer->Release();
	dev->Release();
	devcon->Release();
}

//-----------------------------------------------------------------------------

extern "C" __declspec(dllexport) void RunTestWindow(HMODULE hModule, HWND _phwd ) {

	appInstance = hModule;
	parentWnd = _phwd;
	RECT rect;
	GetWindowRect(parentWnd, &rect);
	d3dWidth = rect.right - rect.left;
	d3dHeight = rect.bottom - rect.top;
	CreateThread(0, NULL, ThreadProc, (LPVOID)NULL, NULL, NULL);		
}

extern "C" __declspec(dllexport) void CloseTestWindow() {

	SendMessage(d3dWnd, WM_CLOSE, NULL, NULL);	
}

extern "C" __declspec(dllexport) void ResizeTestWindow(int width, int height) {

	d3dWidth = width;
	d3dHeight = height;
	SetWindowPos(d3dWnd, HWND_TOP, 0, 0, width, height, NULL);
}
