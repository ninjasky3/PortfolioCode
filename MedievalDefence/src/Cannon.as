package  
{
	import adobe.utils.CustomActions;
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.events.KeyboardEvent;
	import flash.events.Event;
	import flash.media.Sound;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import Player;
	/**
	 * ...
	 * @author kerim birlik && jeremy bond
	 */
	public class Cannon extends MovieClip
	{
		//private var     remover             :RemoverMC 	 = new RemoverMC();
		[Embed(source="../lib/sfx_cannon_shot_01.mp3")]
		public  var     cannonshoot:Class;
		public var _cannonshoot:Sound = new cannonshoot();
				[Embed(source="../lib/sfx_cannon_hit_tower_01.mp3")]
		public  var     cannonhit:Class;
		public var _cannonhit:Sound = new cannonhit();
				[Embed(source="../lib/sfx_shield_hit_01.mp3")]
		public  var     shieldhit:Class;
		public var _shieldhit:Sound = new shieldhit();
				[Embed(source="../lib/sfx_tower_crash_01.mp3")]
		public  var     towercrash:Class;
		public var _towercrash:Sound = new towercrash();
				[Embed(source="../lib/sfx_ambient_forest_01.mp3")]
		public  var     forest:Class;
		public var _forest:Sound = new forest();
		private var 	tower				:towerbayumMC    	= new towerbayumMC();
		public var 	ballTimer			:Timer;
		private var 	_allCannonBalls		:Array 			= 	[];
		private var 	numberY             :Number         = new Number();
		public	var 	lifes				:int      		= new int();
        public  var     player              :Player     	= new Player();
		public 	var 	cannon				:CannonMC 		= new CannonMC();
		private var 	ncb 				:Class		 	= CannonBallMC;
		public function Cannon() 
		{
			_forest.play();
			addEventListener(Event.ENTER_FRAME, loop);
			ballTimer = new Timer(2000, 0);
			ballTimer.addEventListener	(TimerEvent.TIMER, 		shootingBallz);

			player.x    =   50;
			cannon.x 	= 	760;
			cannon.y 	= 	500;
			lifes       =   5;
			tower.y     =   70;

			addChild(tower);

			addChild(cannon);
			addChild(player);
		}
		
		private function loop(e:Event):void 
		{
			for each(var a:MovieClip in _allCannonBalls) {
				
				a.x 	-= 		10;
				a.y 	-= 		numberY 	*	8;

			}
			
			var l:int = _allCannonBalls.length-1;
			trace(Player.player.y);
			for (var i:int = 0; i < l; i++) 
			{
			    if (_allCannonBalls[i].hitTestObject(Player.player)) {
					_shieldhit.play();
					removeChild(_allCannonBalls[i]);
					_allCannonBalls.splice(i, 1);
					
				}
				else if (_allCannonBalls[i].hitTestObject(tower)) 
				{
					removeChild(_allCannonBalls[i]);
					_allCannonBalls.splice(i, 1);
					_cannonhit.play();
					lifes --;
				} 
			} 
			if (lifes == 0) {
				//_towercrash.play();
				while (Main._stage.numChildren > 0) {
						Main._stage.removeChildAt(0);
						player = null;
						cannon = null;
						ballTimer.stop();
				}
			}
		}
		
		private function shootingBallz(e:Event):void 
		{
			_cannonshoot.play();
			var l:int = _allCannonBalls.length - 1;
			var a:MovieClip = new ncb()
			_allCannonBalls.push(a);
			addChild(a);
			a.x = cannon.x;
			a.y = cannon.y;
							
				numberY	= Math.random();
		}
		
		
	}

}