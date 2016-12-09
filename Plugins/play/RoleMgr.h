#ifndef __ROLE_MGR_H_
#define __ROLE_MGR_H_

#include "Role.h"
#include "Player.h"

class Context;
class RoleMgr {
public:
	RoleMgr(Context *ctx);
	~RoleMgr();

	Role * createRole(Player *p);
	void releaseRole(Role **self);

private:
	Context *_ctx;
	Role    *_slots;
	int      _size;
	int      _free;
	int      _cap;
};

#endif