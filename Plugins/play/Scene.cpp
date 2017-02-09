#include "stdafx.h"
#include "Scene.h"
#include "Player.h"
#include "PlayerMgr.h"
#include "Map.h"

#include <foundation/Px.h>
#include <extensions/PxExtensionsAPI.h>
#include <extensions/PxDefaultCpuDispatcher.h>
#include <common/PxTolerancesScale.h>
#include <PxRigidDynamic.h>

#include <cassert>

using namespace physx;

Scene::Scene(Context *ctx)
	: _ctx(ctx)	
{
	PxPhysics  *physics = _ctx->getPhysics();
	PxMaterial *material = physics->createMaterial(0.5f, 0.5f, 0.1f);
	physx::PxSceneDesc sceneDesc(physics->getTolerancesScale());
	sceneDesc.gravity = PxVec3(0.0f, -9.81f, 0.0f);

	if (!sceneDesc.cpuDispatcher) {
		physx::PxCpuDispatcher *dispatcher = PxDefaultCpuDispatcherCreate(1);
		if (!dispatcher) {
			assert(false);
		}
		sceneDesc.cpuDispatcher = dispatcher;
	}

	if (!sceneDesc.filterShader) {
		sceneDesc.filterShader = PxDefaultSimulationFilterShader;
	}

	sceneDesc.flags |= PxSceneFlag::eENABLE_ACTIVETRANSFORMS;
	//sceneDesc.simulationOrder = PxSimulationOrder::eSOLVE_COLLIDE;

	_scene = physics->createScene(sceneDesc);
}


Scene::~Scene() {

}

void Scene::onInit() {
	_scene->userData = this;

	_scene->setSimulationEventCallback(this);

	_playerMgr = _ctx->getPlayerMgr();

	_map = createMap();
}

void Scene::update(float delta) {
	_scene->simulate(delta);
	_scene->fetchResults(true);

	_playerMgr->foreach([](Player *p) {
		PxTransform trans = p->getRigid()->getGlobalPose();
		printf("x:%f , y:%f, z:%f", trans.p.x, trans.p.y, trans.p.z);
	});
}

Map * Scene::createMap() {
	return new Map(_ctx, this);
}

void Scene::releaseMap(Map *m) {
	if (m) {
		delete m;
	}
}

Player * Scene::createPlayer(int uid, int sid) {
	Player *p = _playerMgr->createPlayer();
	p->setUid(uid);
	p->setSid(sid);
	return p;
}

void Scene::releasePlayer(Player *p) {
	if (p != nullptr) {
		_playerMgr->releasePlayer(&p);
	}
}

void Scene::onConstraintBreak(PxConstraintInfo* constraints, PxU32 count) {
}

void Scene::onWake(PxActor** actors, PxU32 count) {
}

void Scene::onSleep(PxActor** actors, PxU32 count) {
}

void Scene::onContact(const PxContactPairHeader& pairHeader, const PxContactPair* pairs, PxU32 nbPairs) {
}

void Scene::onTrigger(PxTriggerPair* pairs, PxU32 count) {
}

void Scene::onAdvance(const PxRigidBody*const* bodyBuffer, const PxTransform* poseBuffer, const PxU32 count) {
}