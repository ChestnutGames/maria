#ifndef __CONTEXT_H_
#define __CONTEXT_H_

#include "PlayerMgr.h"
#include "RoleMgr.h"
#include "Role.h"

#include <extensions/PxDefaultErrorCallback.h>
#include <extensions/PxDefaultAllocator.h>
#include <extensions/PxDefaultCpuDispatcher.h>
#include <foundation/PxAllocatorCallback.h>
#include <foundation/PxFoundation.h>
#include <physxprofilesdk/PxProfileZoneManager.h>
#include <cooking/PxCooking.h>
#include <PxPhysics.h>
#include <PxScene.h>
#include <PxActor.h>
#include <PxRigidDynamic.h>
#include <PxRigidStatic.h>
#include <PxMaterial.h>

#include <map>

class Context {
public:
	Context();
	~Context();

	inline PlayerMgr * getPlayerMgr() const { return _playerMgr; }
	inline RoleMgr * getRoleMgr() const { return _roleMgr; }

	

	void update();

	inline physx::PxPhysics * getPhysics() const { return _physics; }

private:
	PlayerMgr              *_playerMgr;
	RoleMgr                *_roleMgr;

	bool                            _recordMem;
	physx::PxDefaultAllocator       _allocator;
	physx::PxDefaultErrorCallback   _error;
	physx::PxFoundation            *_foundation;
	physx::PxProfileZoneManager    *_profileZoneManager;
	physx::PxPhysics               *_physics;
	physx::PxCooking               *_cooking;
	physx::PxDefaultCpuDispatcher  *_dispatcher;
	physx::PxScene                 *_scene;
	physx::PxRigidStatic           *_plane;
	//physx::PxRigidDynamic          *_a;

};

#endif