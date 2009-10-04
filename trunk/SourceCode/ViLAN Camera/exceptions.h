#ifndef _EXCEPTIONS_H_
#define _EXCEPTIONS_S_

#include <string.h>
#include "pgrflycapture.h"

#define MAX_ERROR_MESSAGE_SIZE 512
#define null 0

class CException
{
public:
	CException(const char *msg = "");
	~CException();

	const char *GetMessage();

	void SetMessage(const char *msg);

private:
	char m_sMessage[MAX_ERROR_MESSAGE_SIZE];
};

class CFlyCapException : public CException
{
public:
	CFlyCapException(FlyCaptureError e = FLYCAPTURE_OK);
	~CFlyCapException();

	void SetFlyCapError(FlyCaptureError error);

private:
	FlyCaptureError m_eError;

};

#endif