package 
{
	import adobe.utils.CustomActions;
	
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.display.Stage;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	
	import flash.automation.StageCaptureEvent;
	import flash.display.FocusDirection;
	import flash.display.StageScaleMode;
	import flash.display.StageDisplayState;
	import flash.events.TimerEvent;
	import flash.geom.Point;
	import flash.sampler.NewObjectSample;
	import flash.utils.Timer;
	
	import Player;
	import CreditsScreen;
	import BeginScreen;
	////////////////////////////////////////////////////////er spawnen cannonballs de we niet zien
	////////////////////////////////////////////////////////hittest op player werkt niet meer maar wel up tower(wut?)
	/**
	 * ...
	 * @author kerim birlik && jeremy bond
	 * ...
	 * probeer de main zo leeg mogelijk te houden
	 * ...
	 */
	public class Main extends Sprite 
	{
		//SCREENS
		public              var score               :int;
		public 				var _startScreen		:BeginScreen 		= new BeginScreen();
		public 				var	_creditsScreen		:CreditsScreen 		= new CreditsScreen();
		public              var ground              :GroundMC           = new GroundMC();
		//STAGE
		public static		var _stage				:Stage;
		//TIMERS
		private             var scoretimer          :Timer;
		//OTHER OBJECTS	
		private 			var cannon				:Cannon				= new Cannon();
		private 			var bgph				:background			= new background();
		
		
		public function Main():void 
		{
			if (stage) init();
			else addEventListener	(Event.ADDED_TO_STAGE, 		init);
		}
	  
		private function init(e:Event = null):void 
		{
			
			_stage 		= 	stage;
			removeEventListener		(Event.	ADDED_TO_STAGE, 	init);
			addChild				(_startScreen);
			
			_startScreen.addEventListener		(BeginScreen.START, 		startGame);
			_startScreen.addEventListener		(BeginScreen.CREDITS, 		goCredits);
			_creditsScreen.addEventListener		(CreditsScreen.BACK, 		goToStartScreen);
		}
		
		private function addingscore(e:Event):void 
		{
			score++;
			//trace(score);
		}
		/////////////// van start scherm gewoon BEGINNEN
		private function startGame(e:Event):void
		{
			cannon.ballTimer.start();
			removeChild			(_startScreen);
			score   	=       new int();
			ground.y    =       550;
			ground.x    =       75;
			addChild			(bgph);//bgph = back ground place holder
			addChild            (ground);
			addChild			(cannon);
		}
		/////////////// van start scherm naar credits scherm
		private function goCredits(e:Event):void
		{
			removeChild		(_startScreen);
			addChild		(_creditsScreen);
		}
		/////////////// van credits scherm terug naar start scherm
		private function goToStartScreen(e:Event):void 
		{
			removeChild(_creditsScreen);
			addChild(_startScreen);
		}
		
		
		
	}
}