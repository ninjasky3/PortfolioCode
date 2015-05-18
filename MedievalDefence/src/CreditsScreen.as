package  
{
	import adobe.utils.CustomActions;
	
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.system.fscommand;
	
	import Main;
	
	/**
	 * ...
	 * @author kerim birlik && jeremy bond
	 */
	public class CreditsScreen extends CreditsScreenMC
	{
		public static const BACK	:	String 	=	"back"; 
		
		public function CreditsScreen() 
		{
			BackBtn.addEventListener		(MouseEvent.CLICK, BackClick);
		}
		
		private function BackClick(e:MouseEvent):void 
		{
			dispatchEvent(new Event(BACK));
			
		}
		
	}

}