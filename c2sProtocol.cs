// Generated by sprotodump. DO NOT EDIT!
using System;
using Sproto;
using System.Collections.Generic;

public class C2sProtocol : ProtocolBase {
	public static  C2sProtocol Instance = new C2sProtocol();
	private C2sProtocol() {
		Protocol.SetProtocol<BeginGUQNQIACoreFight> (BeginGUQNQIACoreFight.Tag);
		Protocol.SetRequest<C2sSprotoType.BeginGUQNQIACoreFight.request> (BeginGUQNQIACoreFight.Tag);
		Protocol.SetResponse<C2sSprotoType.BeginGUQNQIACoreFight.response> (BeginGUQNQIACoreFight.Tag);

		Protocol.SetProtocol<GuanQiaBattleList> (GuanQiaBattleList.Tag);
		Protocol.SetRequest<C2sSprotoType.GuanQiaBattleList.request> (GuanQiaBattleList.Tag);
		Protocol.SetResponse<C2sSprotoType.GuanQiaBattleList.response> (GuanQiaBattleList.Tag);

		Protocol.SetProtocol<achievement> (achievement.Tag);
		Protocol.SetResponse<C2sSprotoType.achievement.response> (achievement.Tag);

		Protocol.SetProtocol<achievement_reward_collect> (achievement_reward_collect.Tag);
		Protocol.SetRequest<C2sSprotoType.achievement_reward_collect.request> (achievement_reward_collect.Tag);
		Protocol.SetResponse<C2sSprotoType.achievement_reward_collect.response> (achievement_reward_collect.Tag);

		Protocol.SetProtocol<app_backgroud> (app_backgroud.Tag);
		Protocol.SetResponse<C2sSprotoType.app_backgroud.response> (app_backgroud.Tag);

		Protocol.SetProtocol<app_resume> (app_resume.Tag);
		Protocol.SetResponse<C2sSprotoType.app_resume.response> (app_resume.Tag);

		Protocol.SetProtocol<applied_list> (applied_list.Tag);
		Protocol.SetResponse<C2sSprotoType.applied_list.response> (applied_list.Tag);

		Protocol.SetProtocol<applydraw> (applydraw.Tag);
		Protocol.SetRequest<C2sSprotoType.applydraw.request> (applydraw.Tag);
		Protocol.SetResponse<C2sSprotoType.applydraw.response> (applydraw.Tag);

		Protocol.SetProtocol<applyfriend> (applyfriend.Tag);
		Protocol.SetRequest<C2sSprotoType.applyfriend.request> (applyfriend.Tag);

		Protocol.SetProtocol<ara_bat_clg> (ara_bat_clg.Tag);
		Protocol.SetRequest<C2sSprotoType.ara_bat_clg.request> (ara_bat_clg.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_bat_clg.response> (ara_bat_clg.Tag);

		Protocol.SetProtocol<ara_bat_ovr> (ara_bat_ovr.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_bat_ovr.response> (ara_bat_ovr.Tag);

		Protocol.SetProtocol<ara_clg_tms_purchase> (ara_clg_tms_purchase.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_clg_tms_purchase.response> (ara_clg_tms_purchase.Tag);

		Protocol.SetProtocol<ara_rfh> (ara_rfh.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_rfh.response> (ara_rfh.Tag);

		Protocol.SetProtocol<ara_rnk_reward_collected> (ara_rnk_reward_collected.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_rnk_reward_collected.response> (ara_rnk_reward_collected.Tag);

		Protocol.SetProtocol<ara_worship> (ara_worship.Tag);
		Protocol.SetRequest<C2sSprotoType.ara_worship.request> (ara_worship.Tag);
		Protocol.SetResponse<C2sSprotoType.ara_worship.response> (ara_worship.Tag);

		Protocol.SetProtocol<c_gold> (c_gold.Tag);
		Protocol.SetResponse<C2sSprotoType.c_gold.response> (c_gold.Tag);

		Protocol.SetProtocol<c_gold_once> (c_gold_once.Tag);
		Protocol.SetRequest<C2sSprotoType.c_gold_once.request> (c_gold_once.Tag);
		Protocol.SetResponse<C2sSprotoType.c_gold_once.response> (c_gold_once.Tag);

		Protocol.SetProtocol<checkin> (checkin.Tag);
		Protocol.SetResponse<C2sSprotoType.checkin.response> (checkin.Tag);

		Protocol.SetProtocol<checkin_aday> (checkin_aday.Tag);
		Protocol.SetResponse<C2sSprotoType.checkin_aday.response> (checkin_aday.Tag);

		Protocol.SetProtocol<checkin_reward> (checkin_reward.Tag);
		Protocol.SetRequest<C2sSprotoType.checkin_reward.request> (checkin_reward.Tag);
		Protocol.SetResponse<C2sSprotoType.checkin_reward.response> (checkin_reward.Tag);

		Protocol.SetProtocol<checkpoint_battle_enter> (checkpoint_battle_enter.Tag);
		Protocol.SetRequest<C2sSprotoType.checkpoint_battle_enter.request> (checkpoint_battle_enter.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_battle_enter.response> (checkpoint_battle_enter.Tag);

		Protocol.SetProtocol<checkpoint_battle_exit> (checkpoint_battle_exit.Tag);
		Protocol.SetRequest<C2sSprotoType.checkpoint_battle_exit.request> (checkpoint_battle_exit.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_battle_exit.response> (checkpoint_battle_exit.Tag);

		Protocol.SetProtocol<checkpoint_chapter> (checkpoint_chapter.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_chapter.response> (checkpoint_chapter.Tag);

		Protocol.SetProtocol<checkpoint_exit> (checkpoint_exit.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_exit.response> (checkpoint_exit.Tag);

		Protocol.SetProtocol<checkpoint_hanging> (checkpoint_hanging.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_hanging.response> (checkpoint_hanging.Tag);

		Protocol.SetProtocol<checkpoint_hanging_choose> (checkpoint_hanging_choose.Tag);
		Protocol.SetRequest<C2sSprotoType.checkpoint_hanging_choose.request> (checkpoint_hanging_choose.Tag);
		Protocol.SetResponse<C2sSprotoType.checkpoint_hanging_choose.response> (checkpoint_hanging_choose.Tag);

		Protocol.SetProtocol<choose_role> (choose_role.Tag);
		Protocol.SetRequest<C2sSprotoType.choose_role.request> (choose_role.Tag);
		Protocol.SetResponse<C2sSprotoType.choose_role.response> (choose_role.Tag);

		Protocol.SetProtocol<deletefriend> (deletefriend.Tag);
		Protocol.SetRequest<C2sSprotoType.deletefriend.request> (deletefriend.Tag);
		Protocol.SetResponse<C2sSprotoType.deletefriend.response> (deletefriend.Tag);

		Protocol.SetProtocol<draw> (draw.Tag);
		Protocol.SetResponse<C2sSprotoType.draw.response> (draw.Tag);

		Protocol.SetProtocol<equipment_all> (equipment_all.Tag);
		Protocol.SetResponse<C2sSprotoType.equipment_all.response> (equipment_all.Tag);

		Protocol.SetProtocol<equipment_enhance> (equipment_enhance.Tag);
		Protocol.SetRequest<C2sSprotoType.equipment_enhance.request> (equipment_enhance.Tag);
		Protocol.SetResponse<C2sSprotoType.equipment_enhance.response> (equipment_enhance.Tag);

		Protocol.SetProtocol<exercise> (exercise.Tag);
		Protocol.SetResponse<C2sSprotoType.exercise.response> (exercise.Tag);

		Protocol.SetProtocol<exercise_once> (exercise_once.Tag);
		Protocol.SetRequest<C2sSprotoType.exercise_once.request> (exercise_once.Tag);
		Protocol.SetResponse<C2sSprotoType.exercise_once.response> (exercise_once.Tag);

		Protocol.SetProtocol<findfriend> (findfriend.Tag);
		Protocol.SetRequest<C2sSprotoType.findfriend.request> (findfriend.Tag);
		Protocol.SetResponse<C2sSprotoType.findfriend.response> (findfriend.Tag);

		Protocol.SetProtocol<friend_list> (friend_list.Tag);
		Protocol.SetResponse<C2sSprotoType.friend_list.response> (friend_list.Tag);

		Protocol.SetProtocol<get_lilian_info> (get_lilian_info.Tag);
		Protocol.SetResponse<C2sSprotoType.get_lilian_info.response> (get_lilian_info.Tag);

		Protocol.SetProtocol<handshake> (handshake.Tag);
		Protocol.SetRequest<C2sSprotoType.handshake.request> (handshake.Tag);
		Protocol.SetResponse<C2sSprotoType.handshake.response> (handshake.Tag);

		Protocol.SetProtocol<kungfu> (kungfu.Tag);
		Protocol.SetResponse<C2sSprotoType.kungfu.response> (kungfu.Tag);

		Protocol.SetProtocol<kungfu_chose> (kungfu_chose.Tag);
		Protocol.SetRequest<C2sSprotoType.kungfu_chose.request> (kungfu_chose.Tag);
		Protocol.SetResponse<C2sSprotoType.kungfu_chose.response> (kungfu_chose.Tag);

		Protocol.SetProtocol<kungfu_levelup> (kungfu_levelup.Tag);
		Protocol.SetRequest<C2sSprotoType.kungfu_levelup.request> (kungfu_levelup.Tag);
		Protocol.SetResponse<C2sSprotoType.kungfu_levelup.response> (kungfu_levelup.Tag);

		Protocol.SetProtocol<lilian_get_phy_power> (lilian_get_phy_power.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_get_phy_power.response> (lilian_get_phy_power.Tag);

		Protocol.SetProtocol<lilian_get_reward_list> (lilian_get_reward_list.Tag);
		Protocol.SetRequest<C2sSprotoType.lilian_get_reward_list.request> (lilian_get_reward_list.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_get_reward_list.response> (lilian_get_reward_list.Tag);

		Protocol.SetProtocol<lilian_inc> (lilian_inc.Tag);
		Protocol.SetRequest<C2sSprotoType.lilian_inc.request> (lilian_inc.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_inc.response> (lilian_inc.Tag);

		Protocol.SetProtocol<lilian_purch_phy_power> (lilian_purch_phy_power.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_purch_phy_power.response> (lilian_purch_phy_power.Tag);

		Protocol.SetProtocol<lilian_reset_quanguan> (lilian_reset_quanguan.Tag);
		Protocol.SetRequest<C2sSprotoType.lilian_reset_quanguan.request> (lilian_reset_quanguan.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_reset_quanguan.response> (lilian_reset_quanguan.Tag);

		Protocol.SetProtocol<lilian_rewared_list> (lilian_rewared_list.Tag);
		Protocol.SetRequest<C2sSprotoType.lilian_rewared_list.request> (lilian_rewared_list.Tag);
		Protocol.SetResponse<C2sSprotoType.lilian_rewared_list.response> (lilian_rewared_list.Tag);

		Protocol.SetProtocol<login> (login.Tag);
		Protocol.SetRequest<C2sSprotoType.login.request> (login.Tag);
		Protocol.SetResponse<C2sSprotoType.login.response> (login.Tag);

		Protocol.SetProtocol<logout> (logout.Tag);
		Protocol.SetResponse<C2sSprotoType.logout.response> (logout.Tag);

		Protocol.SetProtocol<mail_delete> (mail_delete.Tag);
		Protocol.SetRequest<C2sSprotoType.mail_delete.request> (mail_delete.Tag);
		Protocol.SetResponse<C2sSprotoType.mail_delete.response> (mail_delete.Tag);

		Protocol.SetProtocol<mail_getreward> (mail_getreward.Tag);
		Protocol.SetRequest<C2sSprotoType.mail_getreward.request> (mail_getreward.Tag);
		Protocol.SetResponse<C2sSprotoType.mail_getreward.response> (mail_getreward.Tag);

		Protocol.SetProtocol<mail_read> (mail_read.Tag);
		Protocol.SetRequest<C2sSprotoType.mail_read.request> (mail_read.Tag);
		Protocol.SetResponse<C2sSprotoType.mail_read.response> (mail_read.Tag);

		Protocol.SetProtocol<mails> (mails.Tag);
		Protocol.SetResponse<C2sSprotoType.mails.response> (mails.Tag);

		Protocol.SetProtocol<otherfriend_list> (otherfriend_list.Tag);
		Protocol.SetResponse<C2sSprotoType.otherfriend_list.response> (otherfriend_list.Tag);

		Protocol.SetProtocol<props> (props.Tag);
		Protocol.SetResponse<C2sSprotoType.props.response> (props.Tag);

		Protocol.SetProtocol<raffle> (raffle.Tag);
		Protocol.SetRequest<C2sSprotoType.raffle.request> (raffle.Tag);
		Protocol.SetResponse<C2sSprotoType.raffle.response> (raffle.Tag);

		Protocol.SetProtocol<recharge_all> (recharge_all.Tag);
		Protocol.SetResponse<C2sSprotoType.recharge_all.response> (recharge_all.Tag);

		Protocol.SetProtocol<recharge_collect> (recharge_collect.Tag);

		Protocol.SetProtocol<recharge_purchase> (recharge_purchase.Tag);
		Protocol.SetRequest<C2sSprotoType.recharge_purchase.request> (recharge_purchase.Tag);
		Protocol.SetResponse<C2sSprotoType.recharge_purchase.response> (recharge_purchase.Tag);

		Protocol.SetProtocol<recharge_reward> (recharge_reward.Tag);

		Protocol.SetProtocol<recharge_vip_reward_all> (recharge_vip_reward_all.Tag);
		Protocol.SetResponse<C2sSprotoType.recharge_vip_reward_all.response> (recharge_vip_reward_all.Tag);

		Protocol.SetProtocol<recharge_vip_reward_collect> (recharge_vip_reward_collect.Tag);
		Protocol.SetRequest<C2sSprotoType.recharge_vip_reward_collect.request> (recharge_vip_reward_collect.Tag);
		Protocol.SetResponse<C2sSprotoType.recharge_vip_reward_collect.response> (recharge_vip_reward_collect.Tag);

		Protocol.SetProtocol<recharge_vip_reward_purchase> (recharge_vip_reward_purchase.Tag);
		Protocol.SetRequest<C2sSprotoType.recharge_vip_reward_purchase.request> (recharge_vip_reward_purchase.Tag);
		Protocol.SetResponse<C2sSprotoType.recharge_vip_reward_purchase.response> (recharge_vip_reward_purchase.Tag);

		Protocol.SetProtocol<recvfriend> (recvfriend.Tag);
		Protocol.SetRequest<C2sSprotoType.recvfriend.request> (recvfriend.Tag);

		Protocol.SetProtocol<recvheart> (recvheart.Tag);
		Protocol.SetRequest<C2sSprotoType.recvheart.request> (recvheart.Tag);
		Protocol.SetResponse<C2sSprotoType.recvheart.response> (recvheart.Tag);

		Protocol.SetProtocol<refusefriend> (refusefriend.Tag);
		Protocol.SetRequest<C2sSprotoType.refusefriend.request> (refusefriend.Tag);

		Protocol.SetProtocol<role_all> (role_all.Tag);
		Protocol.SetResponse<C2sSprotoType.role_all.response> (role_all.Tag);

		Protocol.SetProtocol<role_battle> (role_battle.Tag);
		Protocol.SetRequest<C2sSprotoType.role_battle.request> (role_battle.Tag);
		Protocol.SetResponse<C2sSprotoType.role_battle.response> (role_battle.Tag);

		Protocol.SetProtocol<role_info> (role_info.Tag);
		Protocol.SetRequest<C2sSprotoType.role_info.request> (role_info.Tag);
		Protocol.SetResponse<C2sSprotoType.role_info.response> (role_info.Tag);

		Protocol.SetProtocol<role_recruit> (role_recruit.Tag);
		Protocol.SetRequest<C2sSprotoType.role_recruit.request> (role_recruit.Tag);
		Protocol.SetResponse<C2sSprotoType.role_recruit.response> (role_recruit.Tag);

		Protocol.SetProtocol<role_upgrade_star> (role_upgrade_star.Tag);
		Protocol.SetRequest<C2sSprotoType.role_upgrade_star.request> (role_upgrade_star.Tag);
		Protocol.SetResponse<C2sSprotoType.role_upgrade_star.response> (role_upgrade_star.Tag);

		Protocol.SetProtocol<sendheart> (sendheart.Tag);
		Protocol.SetRequest<C2sSprotoType.sendheart.request> (sendheart.Tag);
		Protocol.SetResponse<C2sSprotoType.sendheart.response> (sendheart.Tag);

		Protocol.SetProtocol<shop_all> (shop_all.Tag);
		Protocol.SetResponse<C2sSprotoType.shop_all.response> (shop_all.Tag);

		Protocol.SetProtocol<shop_purchase> (shop_purchase.Tag);
		Protocol.SetRequest<C2sSprotoType.shop_purchase.request> (shop_purchase.Tag);
		Protocol.SetResponse<C2sSprotoType.shop_purchase.response> (shop_purchase.Tag);

		Protocol.SetProtocol<shop_refresh> (shop_refresh.Tag);
		Protocol.SetRequest<C2sSprotoType.shop_refresh.request> (shop_refresh.Tag);
		Protocol.SetResponse<C2sSprotoType.shop_refresh.response> (shop_refresh.Tag);

		Protocol.SetProtocol<signup> (signup.Tag);
		Protocol.SetRequest<C2sSprotoType.signup.request> (signup.Tag);
		Protocol.SetResponse<C2sSprotoType.signup.response> (signup.Tag);

		Protocol.SetProtocol<start_lilian> (start_lilian.Tag);
		Protocol.SetRequest<C2sSprotoType.start_lilian.request> (start_lilian.Tag);
		Protocol.SetResponse<C2sSprotoType.start_lilian.response> (start_lilian.Tag);

		Protocol.SetProtocol<use_prop> (use_prop.Tag);
		Protocol.SetRequest<C2sSprotoType.use_prop.request> (use_prop.Tag);
		Protocol.SetResponse<C2sSprotoType.use_prop.response> (use_prop.Tag);

		Protocol.SetProtocol<user> (user.Tag);
		Protocol.SetResponse<C2sSprotoType.user.response> (user.Tag);

		Protocol.SetProtocol<user_can_modify_name> (user_can_modify_name.Tag);
		Protocol.SetResponse<C2sSprotoType.user_can_modify_name.response> (user_can_modify_name.Tag);

		Protocol.SetProtocol<user_modify_name> (user_modify_name.Tag);
		Protocol.SetRequest<C2sSprotoType.user_modify_name.request> (user_modify_name.Tag);
		Protocol.SetResponse<C2sSprotoType.user_modify_name.response> (user_modify_name.Tag);

		Protocol.SetProtocol<user_random_name> (user_random_name.Tag);
		Protocol.SetResponse<C2sSprotoType.user_random_name.response> (user_random_name.Tag);

		Protocol.SetProtocol<user_sign> (user_sign.Tag);
		Protocol.SetRequest<C2sSprotoType.user_sign.request> (user_sign.Tag);
		Protocol.SetResponse<C2sSprotoType.user_sign.response> (user_sign.Tag);

		Protocol.SetProtocol<user_upgrade> (user_upgrade.Tag);
		Protocol.SetResponse<C2sSprotoType.user_upgrade.response> (user_upgrade.Tag);

		Protocol.SetProtocol<wake> (wake.Tag);
		Protocol.SetRequest<C2sSprotoType.wake.request> (wake.Tag);
		Protocol.SetResponse<C2sSprotoType.wake.response> (wake.Tag);

		Protocol.SetProtocol<xilian> (xilian.Tag);
		Protocol.SetRequest<C2sSprotoType.xilian.request> (xilian.Tag);
		Protocol.SetResponse<C2sSprotoType.xilian.response> (xilian.Tag);

		Protocol.SetProtocol<xilian_ok> (xilian_ok.Tag);
		Protocol.SetRequest<C2sSprotoType.xilian_ok.request> (xilian_ok.Tag);
		Protocol.SetResponse<C2sSprotoType.xilian_ok.response> (xilian_ok.Tag);

	}

	public class BeginGUQNQIACoreFight {
		public const int Tag = 85;
	}

	public class GuanQiaBattleList {
		public const int Tag = 86;
	}

	public class achievement {
		public const int Tag = 11;
	}

	public class achievement_reward_collect {
		public const int Tag = 40;
	}

	public class app_backgroud {
		public const int Tag = 75;
	}

	public class app_resume {
		public const int Tag = 74;
	}

	public class applied_list {
		public const int Tag = 16;
	}

	public class applydraw {
		public const int Tag = 39;
	}

	public class applyfriend {
		public const int Tag = 19;
	}

	public class ara_bat_clg {
		public const int Tag = 80;
	}

	public class ara_bat_ovr {
		public const int Tag = 79;
	}

	public class ara_clg_tms_purchase {
		public const int Tag = 82;
	}

	public class ara_rfh {
		public const int Tag = 81;
	}

	public class ara_rnk_reward_collected {
		public const int Tag = 84;
	}

	public class ara_worship {
		public const int Tag = 83;
	}

	public class c_gold {
		public const int Tag = 48;
	}

	public class c_gold_once {
		public const int Tag = 49;
	}

	public class checkin {
		public const int Tag = 43;
	}

	public class checkin_aday {
		public const int Tag = 44;
	}

	public class checkin_reward {
		public const int Tag = 45;
	}

	public class checkpoint_battle_enter {
		public const int Tag = 67;
	}

	public class checkpoint_battle_exit {
		public const int Tag = 65;
	}

	public class checkpoint_chapter {
		public const int Tag = 63;
	}

	public class checkpoint_exit {
		public const int Tag = 72;
	}

	public class checkpoint_hanging {
		public const int Tag = 64;
	}

	public class checkpoint_hanging_choose {
		public const int Tag = 66;
	}

	public class choose_role {
		public const int Tag = 7;
	}

	public class deletefriend {
		public const int Tag = 22;
	}

	public class draw {
		public const int Tag = 38;
	}

	public class equipment_all {
		public const int Tag = 51;
	}

	public class equipment_enhance {
		public const int Tag = 50;
	}

	public class exercise {
		public const int Tag = 46;
	}

	public class exercise_once {
		public const int Tag = 47;
	}

	public class findfriend {
		public const int Tag = 18;
	}

	public class friend_list {
		public const int Tag = 15;
	}

	public class get_lilian_info {
		public const int Tag = 68;
	}

	public class handshake {
		public const int Tag = 1;
	}

	public class kungfu {
		public const int Tag = 55;
	}

	public class kungfu_chose {
		public const int Tag = 57;
	}

	public class kungfu_levelup {
		public const int Tag = 56;
	}

	public class lilian_get_phy_power {
		public const int Tag = 70;
	}

	public class lilian_get_reward_list {
		public const int Tag = 71;
	}

	public class lilian_inc {
		public const int Tag = 76;
	}

	public class lilian_purch_phy_power {
		public const int Tag = 73;
	}

	public class lilian_reset_quanguan {
		public const int Tag = 77;
	}

	public class lilian_rewared_list {
		public const int Tag = 78;
	}

	public class login {
		public const int Tag = 5;
	}

	public class logout {
		public const int Tag = 33;
	}

	public class mail_delete {
		public const int Tag = 13;
	}

	public class mail_getreward {
		public const int Tag = 14;
	}

	public class mail_read {
		public const int Tag = 12;
	}

	public class mails {
		public const int Tag = 3;
	}

	public class otherfriend_list {
		public const int Tag = 17;
	}

	public class props {
		public const int Tag = 9;
	}

	public class raffle {
		public const int Tag = 32;
	}

	public class recharge_all {
		public const int Tag = 34;
	}

	public class recharge_collect {
		public const int Tag = 36;
	}

	public class recharge_purchase {
		public const int Tag = 35;
	}

	public class recharge_reward {
		public const int Tag = 37;
	}

	public class recharge_vip_reward_all {
		public const int Tag = 41;
	}

	public class recharge_vip_reward_collect {
		public const int Tag = 42;
	}

	public class recharge_vip_reward_purchase {
		public const int Tag = 60;
	}

	public class recvfriend {
		public const int Tag = 20;
	}

	public class recvheart {
		public const int Tag = 23;
	}

	public class refusefriend {
		public const int Tag = 21;
	}

	public class role_all {
		public const int Tag = 52;
	}

	public class role_battle {
		public const int Tag = 54;
	}

	public class role_info {
		public const int Tag = 2;
	}

	public class role_recruit {
		public const int Tag = 53;
	}

	public class role_upgrade_star {
		public const int Tag = 6;
	}

	public class sendheart {
		public const int Tag = 24;
	}

	public class shop_all {
		public const int Tag = 29;
	}

	public class shop_purchase {
		public const int Tag = 30;
	}

	public class shop_refresh {
		public const int Tag = 31;
	}

	public class signup {
		public const int Tag = 4;
	}

	public class start_lilian {
		public const int Tag = 69;
	}

	public class use_prop {
		public const int Tag = 10;
	}

	public class user {
		public const int Tag = 28;
	}

	public class user_can_modify_name {
		public const int Tag = 25;
	}

	public class user_modify_name {
		public const int Tag = 26;
	}

	public class user_random_name {
		public const int Tag = 59;
	}

	public class user_sign {
		public const int Tag = 58;
	}

	public class user_upgrade {
		public const int Tag = 27;
	}

	public class wake {
		public const int Tag = 8;
	}

	public class xilian {
		public const int Tag = 61;
	}

	public class xilian_ok {
		public const int Tag = 62;
	}

}