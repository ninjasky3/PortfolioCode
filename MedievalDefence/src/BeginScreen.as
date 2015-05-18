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
	public class BeginScreen extends StartScreenMC
	{
		
		public static const START		:	String	=	"start";
		public static const CREDITS		:	String 	=	"credits"; 
		
		public function BeginScreen() 
		{
			StartBtn.addEventListener		(MouseEvent.CLICK, StartClick);
			middleBtn.addEventListener		(MouseEvent.CLICK, CreditsClick);
			exitBtn.addEventListener		(MouseEvent.CLICK, ExitClick);
		}
		
		
		private function StartClick(e:MouseEvent):void 
		{
			dispatchEvent(new Event(START));
		}
		private function CreditsClick(e:MouseEvent):void 
		{
			dispatchEvent(new Event(CREDITS));
		}
		private function ExitClick(e:MouseEvent):void 
		{
			fscommand("quit");
		}
		
		
		
		
		
		
	}

}