#ifndef __APP_H_
#define __APP_H_

#include "stdafx.h"

#include "Context.h"

#include <map>

class PLAY_API App {
public:
	App();
	~App();

	void run();

	void updata();
	int join(void *ud);
	void leave(int id);

private:
	bool                      _exit;
	Context                   _ctx;

};

#endif