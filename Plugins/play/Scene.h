#ifndef __SCENE_H_
#define __SCENE_H_

#include "Player.h"

#include <PxScene.h>
#include <PxSimulationEventCallback.h>
#include <PxContactModifyCallback.h>

class PlayerMgr;
class Map;
class Scene : public physx::PxSimulationEventCallback {
public:
	Scene(Context *ctx);
	~Scene();

	void onInit();
	void update(float delta);

	inline physx::PxScene * getScene() const { return _scene; }

	Map *createMap();
	void releaseMap(Map *m);

	Player *createPlayer(int uid, int sid);
	void releasePlayer(Player *p);

	virtual void onConstraintBreak(physx::PxConstraintInfo* constraints, physx::PxU32 count);
	virtual void onWake(physx::PxActor** actors, physx::PxU32 count);
	virtual void onSleep(physx::PxActor** actors, physx::PxU32 count);
	virtual void onContact(const physx::PxContactPairHeader& pairHeader, const physx::PxContactPair* pairs, physx::PxU32 nbPairs);
	virtual void onTrigger(physx::PxTriggerPair* pairs, physx::PxU32 count);

private:
	Context                 *_ctx;
	physx::PxScene          *_scene;
	Map                     *_map;
	PlayerMgr               *_playerMgr;

};

#endif // !__SCENE_H_


