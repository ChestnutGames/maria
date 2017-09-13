#ifndef __CONTEXT_H_
#define __CONTEXT_H_

#include <PxPhysicsAPI.h>
#include <extensions/PxExtensionsAPI.h>
#include <PxDeletionListener.h>

#include <map>
#include <string>

class Scene;
class PlayerMgr;
class Context : public physx::PxDeletionListener {
public:
	Context();
	~Context();

	void onInit();
	void update(float delta);

	inline physx::PxFoundation * getFoundation() const { return _foundation; }
	inline physx::PxPhysics    * getPhysics() const { return _physics; }
	inline physx::PxCooking    * getCooking() const { return _cooking; }

	inline PlayerMgr * getPlayerMgr() const { return _playerMgr; }

	inline Scene * getScene() const { return _scene; }

	inline physx::PxMaterial * getDefaultMaterial() const { return _material; }

	void info(const char *fmt, ...);
	void warning(const char *fmt, ...);
	void error(const char *fmt, ...);

	virtual void onRelease(const physx::PxBase* observed, void* userData, physx::PxDeletionEventFlag::Enum deletionEvent);

private:
	bool                            _recordMem;
	physx::PxDefaultAllocator       _allocator;
	physx::PxDefaultErrorCallback   _error;
	physx::PxFoundation            *_foundation;
	//physx::PxProfileZoneManager    *_profileZoneManager;
	physx::PxPhysics               *_physics;
	physx::PxCooking               *_cooking;
	physx::PxMaterial              *_material;

	physx::PxPvd*                    _pvd;
	physx::PxPvdTransport*           _pvdTransport;
	physx::PxPvdInstrumentationFlags _pvdFlags;

	//physx::PxDefaultCpuDispatcher  *_dispatcher;
	//physx::PxRigidStatic           *_plane;
	//physx::PxRigidDynamic          *_a;

	Scene                          *_scene;
	PlayerMgr                      *_playerMgr;

};

#endif