#ifndef __CONTEXT_H_
#define __CONTEXT_H_

#include <extensions/PxDefaultErrorCallback.h>
#include <extensions/PxDefaultAllocator.h>
#include <foundation/PxAllocatorCallback.h>
#include <foundation/PxFoundation.h>
#include <physxprofilesdk/PxProfileZoneManager.h>
#include <cooking/PxCooking.h>
#include <PxPhysics.h>
#include <PxMaterial.h>

#include <map>

class Scene;
class PlayerMgr;
class Context {
public:
	Context();
	~Context();

	void onInit();
	void update(float delta);

	inline physx::PxPhysics * getPhysics() const { return _physics; }
	
	inline PlayerMgr * getPlayerMgr() const { return _playerMgr; }

	inline Scene * getScene() const { return _scene; }

	inline physx::PxMaterial * getDefaultMaterial() const { return _material; }

private:
	bool                            _recordMem;
	physx::PxDefaultAllocator       _allocator;
	physx::PxDefaultErrorCallback   _error;
	physx::PxFoundation            *_foundation;
	physx::PxProfileZoneManager    *_profileZoneManager;
	physx::PxPhysics               *_physics;
	physx::PxCooking               *_cooking;

	physx::PxMaterial              *_material;

	//physx::PxDefaultCpuDispatcher  *_dispatcher;
	//physx::PxRigidStatic           *_plane;
	//physx::PxRigidDynamic          *_a;

	Scene                          *_scene;
	PlayerMgr                      *_playerMgr;

};

#endif