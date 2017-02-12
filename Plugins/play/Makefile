PHYSX_INC     := ../../../PhysX-3.4/PhysX_3.4/Include
PHYSX_INC     += ../../../PhysX-3.4/PxShared/include
PHYSX_LIB     := ../../../PhysX-3.4/PhysX_3.4/Lib/linux64
PHYSX_LIB     += ../../../PhysX-3.4/PxShared/lib/linux64
PHYSX_BIN     := ../../../PhysX-3.4/PhysX_3.4/Bin/linux64
PHYSX_BIN     += ../../../PhysX-3.4/PxShared/bin/linux64

CFLAGS    := -g -O2 -Wall
CPPFLAGES := -std=c++11 -fpermissive -Wnarrowing $(CFLAGS)
SHARED    := -fPIC --shared

battle_cppfiles        :=
battle_cppfiles        += App.cpp
battle_cppfiles        += Context.cpp
battle_cppfiles        += Map.cpp
battle_cppfiles        += Player.cpp
battle_cppfiles        += PlayerMgr.cpp
battle_cppfiles        += Scene.cpp
battle_cppfiles        += stdafx.cpp
battle_cppfiles        += playc.cpp
battle_cppfiles        += pack.cpp

battle_debug_defines   := PHYSX_PROFILE_SDK
battle_debug_defines   += _DEBUG
battle_debug_defines   += PX_DEBUG
battle_debug_defines   += PX_CHECKED
battle_debug_defines   += PX_SUPPORT_VISUAL_DEBUGGER

battle_debug_hpaths    := 
battle_debug_hpaths    += $(PHYSX_INC)

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

# battle_debug_libraries += X11
# battle_debug_libraries += Xxf86vm
# battle_debug_libraries += Cg
# battle_debug_libraries += CgGL
# battle_debug_libraries += rt
battle_debug_libraries += pthread
battle_debug_libraries += dl

# debug_common_cflags	 := $(custom_cflags)
battle_debug_common_cflags  := -MMD
battle_debug_common_cflags  += $(addprefix -D, $(battle_debug_defines))
battle_debug_common_cflags  += $(addprefix -I, $(battle_debug_hpaths))
battle_debug_common_cflags  += -m64
# battle_debug_common_cflags  += -Werror -m64 -fPIC -msse2 -mfpmath=sse -ffast-math -fno-exceptions -fno-rtti -fvisibility=hidden -fvisibility-inlines-hidden
# battle_debug_common_cflags  += -Wall -Wextra -Wstrict-aliasing=2 -fdiagnostics-show-option
# battle_debug_common_cflags  += -Wno-long-long
# battle_debug_common_cflags  += -Wno-unknown-pragmas -Wno-char-subscripts -Wno-unused-variable -Wno-reorder -Wno-sign-compare
# battle_debug_common_cflags  += -Wno-unused-parameter
# battle_debug_common_cflags  += -g3 -gdwarf-2
# battle_debug_common_cflags  += -fexceptions
battle_debug_common_cflags  += -g -Wall

OBJS := $(battle_cppfiles:.cpp=.o)
DEPS := $(battle_cppfiles:.cpp=.d)

all: libplay.so libplay.a
	rm -f $(OBJS) $(DEPS)

$(OBJS):%.o:%.cpp
	g++ -std=c++11 -c -fPIC $(battle_debug_common_cflags) $^ -o $@ 

libplay.a: $(OBJS)
	ar crv $@ $^

libplay.so: $(OBJS)
	g++ -std=c++11 $(SHARED) $(battle_debug_common_cflags) -o $@ $^ $(addprefix -L, $(battle_debug_lpaths)) -Wl,-Bdynamic $(addprefix -l, $(battle_debug_libraries))
	
clean:
	rm -f $(OBJS) libplayc.so libplay.a