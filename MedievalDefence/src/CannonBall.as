package  
{
	import adobe.utils.CustomActions;
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.events.DRMCustomProperties;
	import flash.events.KeyboardEvent;
	import flash.events.Event;
	import flash.utils.Timer
	import flash.events.TimerEvent;
	/**
	 * ...
	 * @author kerim birlik && jeremy bond
	 */
	public class CannonBall extends MovieClip
	{
		public 		var cannonBall			:	CannonBallMC 	= 	new CannonBallMC;
		private     var numberY             :   Number          =   new Number();
		public function CannonBall() 
		{
				numberY	= Math.random();
				//addEventListener    (Event.ENTER_FRAME, loop);
				//addChild(cannonBall);
		}
		private function loop(e:Event):void 
		{
 
			cannonBall.x 	-= 		9;
		    cannonBall.y 	-= 		(numberY 	*	3)	+	1;
		}
	}
}