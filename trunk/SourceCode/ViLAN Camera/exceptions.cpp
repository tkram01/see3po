#include "stdafx.h"
#include "exceptions.h"

CException::CException(const char *msg)
{
	strcpy_s(m_sMessage, MAX_ERROR_MESSAGE_SIZE, msg);
}

CException::~CException()
{
}

const char *CException::GetMessage()
{
	return m_sMessage;
}

void CException::SetMessage(const char *msg)
{
	strcpy_s(m_sMessage, MAX_ERROR_MESSAGE_SIZE, msg);
}






CFlyCapException::CFlyCapException(FlyCaptureError e)
{
	SetFlyCapError(e);
}

CFlyCapException::~CFlyCapException()
{
}

void CFlyCapException::SetFlyCapError(FlyCaptureError error)
{
	m_eError = error;

	switch(error)
	{
	case FLYCAPTURE_OK:
		SetMessage("FlyCap: OK");
		break;

	case FLYCAPTURE_ALREADY_INITIALIZED:
		SetMessage("FlyCap: Already Initialized");
		break;

	case FLYCAPTURE_ALREADY_STARTED:
		SetMessage("FlyCap: Already Started");
		break;

	case FLYCAPTURE_CALLBACK_ALREADY_REGISTERED:
		SetMessage("FlyCap: Callback Already Registered");
		break;

	case FLYCAPTURE_CALLBACK_NOT_REGISTERED:
		SetMessage("FlyCap: Callback Not Registered");
		break;

	case FLYCAPTURE_CAMERACONTROL_PROBLEM:
		SetMessage("FlyCap: Camera Control Problem");
		break;

	case FLYCAPTURE_COULD_NOT_OPEN_DEVICE_HANDLE:
		SetMessage("FlyCap: Could Not Open Device Handle");
		break;

	case FLYCAPTURE_COULD_NOT_OPEN_FILE:
		SetMessage("FlyCap: Could Not Open File");
		break;

	case FLYCAPTURE_DEPRECATED:
		SetMessage("FlyCap: Deprecated");
		break;

	case FLYCAPTURE_DEVICE_BUSY:
		SetMessage("FlyCap: Device Busy");
		break;

	case FLYCAPTURE_ERROR_UNKNOWN:
		SetMessage("FlyCap: Unknown Error");
		break;

	case FLYCAPTURE_FAILED:
		SetMessage("FlyCap: Failed");
		break;

	case FLYCAPTURE_INVALID_ARGUMENT:
		SetMessage("FlyCap: Invalid Argument");
		break;

	case FLYCAPTURE_INVALID_CONTEXT:
		SetMessage("FlyCap: Invalid Context");
		break;

	case FLYCAPTURE_INVALID_CUSTOM_SIZE:
		SetMessage("FlyCap: Invalid Custom Size");
		break;

	case FLYCAPTURE_MAX_BANDWIDTH_EXCEEDED:
		SetMessage("FlyCap: Max Bandwidth Exceeded");
		break;

	case FLYCAPTURE_MEMORY_ALLOC_ERROR:
		SetMessage("FlyCap: Memory Allocation Error");
		break;

	case FLYCAPTURE_NON_PGR_CAMERA:
		SetMessage("FlyCap: Non-PGR Camera");
		break;

	case FLYCAPTURE_NOT_IMPLEMENTED:
		SetMessage("FlyCap: Not Implemented");
		break;

	case FLYCAPTURE_NOT_INITIALIZED:
		SetMessage("FlyCap: Not Initialized");
		break;

	case FLYCAPTURE_NOT_STARTED:
		SetMessage("FlyCap: Not Started");
		break;

	case FLYCAPTURE_NO_IMAGE:
		SetMessage("FlyCap: No Image");
		break;

	case FLYCAPTURE_TIMEOUT:
		SetMessage("FlyCap: Timeout");
		break;

	case FLYCAPTURE_TOO_MANY_LOCKED_BUFFERS:
		SetMessage("FlyCap: Too Many Locked Buffers");
		break;

	case FLYCAPTURE_VERSION_MISMATCH:
		SetMessage("FlyCap: Version Mismatch");
		break;


	default:
		SetMessage("Unknown Error");
		break;
	}
}
