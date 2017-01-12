#ifndef __APP_H_
#define __APP_H_

#include "stdafx.h"

#include "Context.h"

#include <map>
#include <unordered_map>

class PLAY_API App {
public:
	App();
	~App();

	void run();

	void start();
	void close() {}
	void kill() {}

	void updata(float delta);
	bool join(int suid, int sid);
	void leave(int suid, int sid);
	void opcode();

private:
	bool                      _exit;
	Context                   _ctx;
};

#endif
