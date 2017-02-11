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

	void update(float delta);
	bool join(int suid, int sid, int session);
	void leave(int suid, int sid, int session);
	void opcode();
	int fetch(char *ptr, int len);

private:
	bool                      _exit;
	Context                   _ctx;
};

#endif
