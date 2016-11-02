LOCAL_PATH := $(call my-dir)
include $(CLEAR_VARS)
LOCAL_MODULE := rudp
LOCAL_MODULE_FILENAME := librudp
LOCAL_SRC_FILES := ./../stdafx.c \
                   ./../rudp.c \
LOCAL_C_INCLUDES := $(LOCAL_PATH)/../
           
include $(BUILD_SHARED_LIBRARY)