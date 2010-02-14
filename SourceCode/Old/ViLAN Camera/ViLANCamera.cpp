#include "stdafx.h"
#include "ViLANCamera.h"

namespace ViLANCameras
{
	CViLANCamera::CViLANCamera(int index)
	{
		m_Base = new _CViLANCamera(index);
	}

	CViLANCamera::~CViLANCamera()
	{
		delete m_Base;
	}

	bool CViLANCamera::InitializeCamera()
	{
		return m_Base->InitializeCamera();
	}

	bool CViLANCamera::StartImage(EImageMode mode)
	{
		return m_Base->StartImage(mode);
	}

	bool CViLANCamera::Stop()
	{
		return m_Base->Stop();
	}

	int CViLANCamera::GetBufferSize()
	{
		return m_Base->GetBufferSize();
	}

	bool CViLANCamera::CaptureImageToFile(String^ filename, EImageFileFormat format)
	{
		return m_Base->CaptureImageToFile(CString(filename).GetString(), format);
	}

	bool CViLANCamera::CaptureImageToDevice(IntPtr ptr)
	{
		return m_Base->CaptureImageToDevice((HDC)ptr.ToPointer());
	}

	bool CViLANCamera::CaptureImage(int address)
	{
		return m_Base->CaptureImage((unsigned char *)address);
	}

	bool CViLANCamera::StartStream(IntPtr ptr)
	{
		return m_Base->StartStream((HDC)ptr.ToPointer());
	}

	bool CViLANCamera::StreamNext()
	{
		return m_Base->StreamNext();
	}

	bool CViLANCamera::StreamNext(int address)
	{
		return m_Base->StreamNext((unsigned char *)address);
	}

	String^ CViLANCamera::GetErrorMessage()
	{
		return gcnew String(m_Base->GetErrorMessage());
	}
}







_CViLANCamera::_CViLANCamera(int index)
{
	m_iCameraIndex = index;
	m_Context = null;

	m_BitmapInfoHeader = &m_BitmapInfo.bmiHeader;
	m_BitmapInfoHeader->biSize = sizeof(BITMAPINFOHEADER);
	m_BitmapInfoHeader->biPlanes = 1;
	m_BitmapInfoHeader->biCompression = BI_RGB;
	m_BitmapInfoHeader->biXPelsPerMeter = 100;
	m_BitmapInfoHeader->biYPelsPerMeter = 100;
	m_BitmapInfoHeader->biClrUsed = 0;
	m_BitmapInfoHeader->biClrImportant = 0;

	m_eCameraMode = ECameraMode::CM_UNINITIALIZED;
	m_eImageMode = EImageMode::NA;

	error = FLYCAPTURE_OK;
	m_Error = new CFlyCapException();
}

_CViLANCamera::~_CViLANCamera()
{
	switch(m_eCameraMode)
	{
	case ECameraMode::CM_CAPTURE_FREE:
	case ECameraMode::CM_CAPTURE_STREAM:
		Stop();

	case ECameraMode::CM_IDLE:
		flycaptureDestroyContext(m_Context);
		break;
	}
}

bool _CViLANCamera::InitializeCamera()
{
	error = flycaptureCreateContext(&m_Context);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureInitialize(m_Context, m_iCameraIndex);
	HANDLE_FLYCAP_ERROR(error);

	m_eCameraMode = ECameraMode::CM_IDLE;

	return true;
}

bool _CViLANCamera::StartImage(EImageMode mode)
{
	if(m_eCameraMode != ECameraMode::CM_IDLE)
	{
		m_Error->SetMessage("Start*ImageMode() requires camera to be in idle mode.");
		return false;
	}

	switch(mode)
	{
	case EImageMode::SIM_1024x768_YUV422_15:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_1024x768YUV422, FLYCAPTURE_FRAMERATE_15);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 1024;
		m_BitmapInfoHeader->biHeight = 768;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[1024 * 768 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	case EImageMode::SIM_1024x768_YUV422_7p5:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_1024x768YUV422, FLYCAPTURE_FRAMERATE_7_5);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 1024;
		m_BitmapInfoHeader->biHeight = 768;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[1024 * 768 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	case EImageMode::SIM_640x480_RGB_30:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_640x480RGB, FLYCAPTURE_FRAMERATE_30);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 640;
		m_BitmapInfoHeader->biHeight = 480;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[640 * 480 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	case EImageMode::SIM_640x480_RGB_15:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_640x480RGB, FLYCAPTURE_FRAMERATE_15);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 640;
		m_BitmapInfoHeader->biHeight = 480;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[640 * 480 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	case EImageMode::SIM_640x480_RGB_7p5:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_640x480RGB, FLYCAPTURE_FRAMERATE_7_5);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 640;
		m_BitmapInfoHeader->biHeight = 480;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[640 * 480 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	case EImageMode::SIM_320x240_YUV422_30:
		error = flycaptureStart(m_Context, FLYCAPTURE_VIDEOMODE_320x240YUV422, FLYCAPTURE_FRAMERATE_30);
		HANDLE_FLYCAP_ERROR(error);

		m_BitmapInfoHeader->biWidth = 320;
		m_BitmapInfoHeader->biHeight = 240;
		m_BitmapInfoHeader->biBitCount = 32;
		m_BitmapInfoHeader->biSizeImage = m_BitmapInfoHeader->biWidth * m_BitmapInfoHeader->biHeight * (m_BitmapInfoHeader->biBitCount / 8);

		m_ConvertedImage.pData = new unsigned char[320 * 240 * 4];
		m_ConvertedImage.pixelFormat = FLYCAPTURE_BGRU;
		break;

	default:
		m_Error->SetMessage("Unknown image mode specified.");
		return false;
	}

	m_eCameraMode = ECameraMode::CM_CAPTURE_FREE;
	m_eImageMode = mode;

	return true;
}

bool _CViLANCamera::Stop()
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_FREE && m_eCameraMode != ECameraMode::CM_CAPTURE_STREAM)
	{
		m_Error->SetMessage("Stop() requires camera to be in capture mode.");
		return false;
	}

	delete[] m_ConvertedImage.pData;

	error = flycaptureStop(m_Context);
	HANDLE_FLYCAP_ERROR(error);

	return true;
}

int _CViLANCamera::GetBufferSize()
{
	switch(m_eImageMode)
	{
	case EImageMode::SIM_1024x768_YUV422_15:
		return (1024*768*4);

	case EImageMode::SIM_1024x768_YUV422_7p5:
		return (1024*768*4);

	case EImageMode::SIM_640x480_RGB_30:
		return (640*480*4);

	case EImageMode::SIM_640x480_RGB_15:
		return (640*480*4);

	case EImageMode::SIM_640x480_RGB_7p5:
		return (640*480*4);

	case EImageMode::SIM_320x240_YUV422_30:
		return (320*240*4);

	default:
		m_Error->SetMessage("Unknown image mode specified.");
		return -1;
	}
}

bool _CViLANCamera::CaptureImageToFile(const char *filename, EImageFileFormat format)
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_FREE)
	{
		m_Error->SetMessage("CaptureImage*() requires camera to be in capture-free mode.");
		return false;
	}

	error = flycaptureGrabImage2(m_Context, &m_RawImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureConvertImage(m_Context, &m_RawImage, &m_ConvertedImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureSaveImage(m_Context, &m_ConvertedImage, filename, (FlyCaptureImageFileFormat)format);
	HANDLE_FLYCAP_ERROR(error);

	return true;
}

bool _CViLANCamera::CaptureImageToDevice(HDC hdc)
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_FREE)
	{
		m_Error->SetMessage("CaptureImage*() requires camera to be in capture-free mode.");
		return false;
	}

	error = flycaptureGrabImage2(m_Context, &m_RawImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureConvertImage(m_Context, &m_RawImage, &m_ConvertedImage);
	HANDLE_FLYCAP_ERROR(error);

	SetDIBitsToDevice(hdc, 0, 0, m_BitmapInfoHeader->biWidth, abs(m_BitmapInfoHeader->biHeight), 0, 0, 0, abs(m_BitmapInfoHeader->biWidth), m_ConvertedImage.pData, &m_BitmapInfo, DIB_RGB_COLORS);

	return true;
}

bool _CViLANCamera::CaptureImage(unsigned char *data)
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_FREE)
	{
		m_Error->SetMessage("CaptureImage*() requires camera to be in capture-free mode.");
		return false;
	}

	error = flycaptureGrabImage2(m_Context, &m_RawImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureConvertImage(m_Context, &m_RawImage, &m_ConvertedImage);
	HANDLE_FLYCAP_ERROR(error);

	memcpy_s(data, m_BitmapInfoHeader->biSizeImage, m_ConvertedImage.pData, m_BitmapInfoHeader->biSizeImage);

	return true;
}

bool _CViLANCamera::StartStream(HDC hdc)
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_FREE)
	{
		m_Error->SetMessage("SetupStream() requires camera to be in capture-free mode.");
		return false;
	}

	m_StreamHdc = hdc;
	m_eCameraMode = ECameraMode::CM_CAPTURE_STREAM;

	return true;
}

bool _CViLANCamera::StreamNext()
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_STREAM)
	{
		m_Error->SetMessage("StreamNext() requires camera to be in capture-stream mode.");
		return false;
	}

	error = flycaptureGrabImage2(m_Context, &m_RawImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureConvertImage(m_Context, &m_RawImage, &m_ConvertedImage);
	HANDLE_FLYCAP_ERROR(error);

	SetDIBitsToDevice(m_StreamHdc, 0, 0, m_BitmapInfoHeader->biWidth, abs(m_BitmapInfoHeader->biHeight), 0, 0, 0, abs(m_BitmapInfoHeader->biWidth), m_ConvertedImage.pData, &m_BitmapInfo, DIB_RGB_COLORS);

	return true;
}

bool _CViLANCamera::StreamNext(unsigned char *buffer)
{
	if(m_eCameraMode != ECameraMode::CM_CAPTURE_STREAM)
	{
		m_Error->SetMessage("StreamNext() requires camera to be in capture-stream mode.");
		return false;
	}

	error = flycaptureGrabImage2(m_Context, &m_RawImage);
	HANDLE_FLYCAP_ERROR(error);

	error = flycaptureConvertImage(m_Context, &m_RawImage, &m_ConvertedImage);
	HANDLE_FLYCAP_ERROR(error);

	SetDIBitsToDevice(m_StreamHdc, 0, 0, m_BitmapInfoHeader->biWidth, abs(m_BitmapInfoHeader->biHeight), 0, 0, 0, abs(m_BitmapInfoHeader->biWidth), m_ConvertedImage.pData, &m_BitmapInfo, DIB_RGB_COLORS);

	return true;
}

const char *_CViLANCamera::GetErrorMessage()
{
	return m_Error->GetMessage();
}
