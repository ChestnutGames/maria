#ifndef __PLAYER_H_
#define __PLAYER_H_

namespace physx {
	class PxRigidDynamic;
}
class Context;
class Player {
public:
	Player();
	Player(Context *ctx);
	~Player();

	void setNext(Player *next) { _next = next; }
	Player* getNext() const { return _next; }

	inline int getUid() const { return _uid; }
	inline void setUid(int value) { _uid = value; }
	inline int getSid() const { return _sid; }
	inline void setSid(int value) { _sid = value; }

	void createRigid();
	void releaseRigid();

	physx::PxRigidDynamic * getRigid() const { return _rigid; }

private:
	Context *_ctx;
	Player  *_next;

	int   _uid;
	int   _sid;
	physx::PxRigidDynamic *_rigid;
};

#endif