#ifndef __ROLE_H_
#define __ROLE_H_

#include <PxRigidDynamic.h>

class Context;
class Player;
class Role {
public:
	Role();
	Role(Context *ctx, Player *p);
	~Role();

	inline Player * getPlayer() const { return _player; }
	inline void setPlayer(Player *p) { _player = p; }

	inline physx::PxRigidDynamic * getRigid() const { return _rigid; }

private:

	Context               *_ctx;
	Player                *_player;
	physx::PxRigidDynamic *_rigid;

};

#endif