#ifndef __APP_H_
#define __APP_H_

#include "stdafx.h"
#include "Context.h"

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
#include <unordered_map>

class PLAY_API App {
public:
	App();
	~App();

	void run();

	void updata(float delta);
	int join(void *ud);
	void leave(int id);
	void opcode();

private:
	bool                      _exit;
	Context                   _ctx;
	std::map<int, Player *>   _players;
};

#endif
