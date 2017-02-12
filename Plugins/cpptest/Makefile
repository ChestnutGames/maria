PHYSX_INC     := ../../../PhysX-3.4/PhysX_3.4/Include
PHYSX_INC     += ../../../PhysX-3.4/PxShared/include
PHYSX_LIB     := ../../../PhysX-3.4/PhysX_3.4/Lib/linux64
PHYSX_LIB     += ../../../PhysX-3.4/PxShared/lib/linux64
PHYSX_BIN     := ../../../PhysX-3.4/PhysX_3.4/Bin/linux64
PHYSX_BIN     += ../../../PhysX-3.4/PxShared/bin/linux64


battle_debug_lpaths    := $(PHYSX_LIB)
battle_debug_lpaths    += $(PHYSX_BIN)

battle_debug_libraries :=
battle_debug_libraries += PxFoundationDEBUG_x64
battle_debug_libraries += PxPvdSDKDEBUG_x64
battle_debug_libraries += PsFastXmlDEBUG
battle_debug_libraries += PxTaskDEBUG
battle_debug_libraries += PhysX3ExtensionsDEBUG
battle_debug_libraries += PhysX3CommonDEBUG_x64
battle_debug_libraries += PhysX3CookingDEBUG_x64
battle_debug_libraries += PhysX3DEBUG_x64

cpptest: main.cpp ../sharpc/play.cpp
	g++ -std=c++11 -g -Wall -o $@ $^ -I../play -I../sharpc -L../play -Wl,-Bdynamic -lplay $(addprefix -L, $(battle_debug_lpaths)) -Wl,-Bdynamic $(addprefix -l, $(battle_debug_libraries))

clean:
	rm -f cpptest