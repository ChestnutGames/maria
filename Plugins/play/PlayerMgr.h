#ifndef __PLAYER_MGR_H_
#define __PLAYER_MGR_H_

#include "Player.h"

class Context;
class PlayerMgr {
public:
	PlayerMgr(Context *ctx);
	~PlayerMgr();

	Player * createPlayer(void *ud);
	void releasePlayer(Player **self);

private:
	Context *_ctx;

	Player *_slots;
	int     _size;
	int     _free;
	int     _cap;

	int     _idx;
};

#endif