#ifndef __PLAYER_MGR_H_
#define __PLAYER_MGR_H_

#include "Player.h"

#include <map>
#include <unordered_map>
#include <functional>

class Context;
class PlayerMgr {
public:
	PlayerMgr(Context *ctx);
	~PlayerMgr();

	Player * getPlayerBySuid(int suid);
	Player * getPlayerBySid(int sid);

	void addPlayerByUid(Player *p);
	void removePlayerByUid(int suid);

	void addPlayerBySid(Player *p);
	void removePlayerBySid(int sid);

	void addPlayerBySession(Player *p);
	void removePlayerBySession(int session);

	void addPlayer(Player *p);
	void removePlayer(Player *p);

	Player * createPlayer();
	void releasePlayer(Player **self);

	void foreach(std::function<void(Player*)> &&cb);

private:
	
	Context *_ctx;

	Player *_freelist;
	Player *_slots;
	int     _size;
	int     _cap;
	int     _free;
	
	std::list<Player*>               _players;
	std::unordered_map<int, Player*> _suplayers;
	std::unordered_map<int, Player*> _splayers;
	std::unordered_map<int, Player*> _seplayers;

};

#endif