#ifndef __PLAYER_H_
#define __PLAYER_H_

#include <PxPhysicsAPI.h>

namespace physx {
	class PxRigidDynamic;
}

class Context;
class Player {
public:
	Player();
	Player(Context *ctx);
	~Player();

	void                        setNext(Player *next) { _next = next; }
	Player*                     getNext() const { return _next; }

	inline int                  getUid() const { return _uid; }
	inline void                 setUid(int value) { _uid = value; }
	inline int                  getSid() const { return _sid; }
	inline void                 setSid(int value) { _sid = value; }

	void                        createRigid();
	void                        releaseRigid();

	physx::PxRigidDynamic*      getRigid() const { return _rigid; }

	physx::PxRigidDynamic*	    createBox(const physx::PxVec3& pos, const physx::PxVec3& dims, const physx::PxVec3* linVel = NULL, physx::PxReal density = 1.0f);
	physx::PxRigidDynamic*		createSphere(const physx::PxVec3& pos, physx::PxReal radius, const physx::PxVec3* linVel = NULL, physx::PxReal density = 1.0f);
	physx::PxRigidDynamic*		createCapsule(const physx::PxVec3& pos, physx::PxReal radius, physx::PxReal halfHeight, const physx::PxVec3* linVel = NULL, physx::PxReal density = 1.0f);
	physx::PxRigidDynamic*		createConvex(const physx::PxVec3& pos, const physx::PxVec3* linVel = NULL, physx::PxReal density = 1.0f);

private:
	Context *_ctx;
	Player  *_next;

	int   _uid;
	int   _sid;
	physx::PxRigidDynamic *_rigid;
};

#endif