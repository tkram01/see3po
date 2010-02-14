#ifndef _ViLAN_CAMERA_H_
#define _ViLAN_CAMERA_H_

#pragma comment (lib, "pgrflycapture.lib")
#pragma comment (lib, "gdi32.lib")

#include <atlstr.h>
#include <windows.h>
#include "stdafx.h"

#include "pgrflycapture.h"
#include "exceptions.h"

using namespace System;

#define HANDLE_FLYCAP_ERROR(x) \
		m_Error->SetFlyCapError(x); \
		if(x != FLYCAPTURE_OK) \
			return false;

#define MAX_CAMS 32

class _CViLANCamera;

namespace ViLANCameras
{
	public enum class ECameraMode
	{
		CM_UNINITIALIZED,
		CM_IDLE,
		CM_CAPTURE_FREE,
		CM_CAPTURE_STREAM
	};

	public enum class EImageMode
	{
		NA,
		SIM_1024x768_YUV422_15,
		SIM_1024x768_YUV422_7p5,
		SIM_640x480_RGB_30,
		SIM_640x480_RGB_15,
		SIM_640x480_RGB_7p5,
		SIM_320x240_YUV422_30
	};

	public enum class EImageFileFormat
	{
		IFF_BITMAP = 2,
		IFF_RAW = 5
	};

	public ref class CViLANCamera
	{
	public:
		CViLANCamera(int index);
		~CViLANCamera();

		bool InitializeCamera();

		bool StartImage(EImageMode mode);
		bool Stop();

		int GetBufferSize();
		bool CaptureImageToFile(String^ filename, EImageFileFormat format);
		bool CaptureImageToDevice(IntPtr ptr);
		bool CaptureImage(int address);

		bool StartStream(IntPtr ptr);
		bool StreamNext();
		bool StreamNext(int address);

		String^ GetErrorMessage();

	private:
		_CViLANCamera *m_Base;

	};
}

using namespace ViLANCameras;

class _CViLANCamera
{
public:
	_CViLANCamera(int index);
	~_CViLANCamera();

	bool InitializeCamera();

	bool StartImage(EImageMode mode);
	bool Stop();

	int GetBufferSize();
	bool CaptureImageToFile(const char* filename, EImageFileFormat format);
	bool CaptureImageToDevice(HDC hdc);
	bool CaptureImage(unsigned char *data);

	bool StartStream(HDC hdc);
	bool StreamNext();
	bool StreamNext(unsigned char *data);

	const char *GetErrorMessage();

private:
	int m_iCameraIndex;

	FlyCaptureContext m_Context;
	FlyCaptureImage m_RawImage;
	FlyCaptureImage m_ConvertedImage;

	BITMAPINFO m_BitmapInfo;
	BITMAPINFOHEADER *m_BitmapInfoHeader;

	ECameraMode m_eCameraMode;
	EImageMode m_eImageMode;

	HDC m_StreamHdc;

	FlyCaptureError error;
	CFlyCapException *m_Error;

};

#endif
