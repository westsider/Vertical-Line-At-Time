#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class VerticlaLineAtTime : Indicator
	{
		private bool LineDrawnToday = false;
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "Verticlal Line At Time";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				LineTime									= DateTime.Parse("08:30", System.Globalization.CultureInfo.InvariantCulture);
				LineColor									= Brushes.DarkRed;
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			Print(ToTime(LineTime));
			if (IsBetween(start: ToTime(LineTime), end: ToTime(LineTime) + 1000) && !LineDrawnToday) {
				LineDrawnToday = true; 
				Draw.VerticalLine(this, "openLine" + CurrentBar, 0, LineColor, DashStyleHelper.Dash, 2, true );
			} 
			if (IsBetween(start: 1500, end: ToTime(LineTime) )) {
				LineDrawnToday = false;
			}
		}
		
		private bool IsBetween(int start, int end) {
			var Now = ToTime(Time[0]) ;
			if (Now > start && Now < end) {
				return true;
			} else { return false; }
		}

		#region Properties
		[NinjaScriptProperty]
		[PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
		[Display(Name="LineTime", Order=1, GroupName="Parameters")]
		public DateTime LineTime
		{ get; set; }

		[NinjaScriptProperty]
		[XmlIgnore]
		[Display(Name="LineColor", Order=2, GroupName="Parameters")]
		public Brush LineColor
		{ get; set; }

		[Browsable(false)]
		public string LineColorSerializable
		{
			get { return Serialize.BrushToString(LineColor); }
			set { LineColor = Serialize.StringToBrush(value); }
		}			
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private VerticlaLineAtTime[] cacheVerticlaLineAtTime;
		public VerticlaLineAtTime VerticlaLineAtTime(DateTime lineTime, Brush lineColor)
		{
			return VerticlaLineAtTime(Input, lineTime, lineColor);
		}

		public VerticlaLineAtTime VerticlaLineAtTime(ISeries<double> input, DateTime lineTime, Brush lineColor)
		{
			if (cacheVerticlaLineAtTime != null)
				for (int idx = 0; idx < cacheVerticlaLineAtTime.Length; idx++)
					if (cacheVerticlaLineAtTime[idx] != null && cacheVerticlaLineAtTime[idx].LineTime == lineTime && cacheVerticlaLineAtTime[idx].LineColor == lineColor && cacheVerticlaLineAtTime[idx].EqualsInput(input))
						return cacheVerticlaLineAtTime[idx];
			return CacheIndicator<VerticlaLineAtTime>(new VerticlaLineAtTime(){ LineTime = lineTime, LineColor = lineColor }, input, ref cacheVerticlaLineAtTime);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.VerticlaLineAtTime VerticlaLineAtTime(DateTime lineTime, Brush lineColor)
		{
			return indicator.VerticlaLineAtTime(Input, lineTime, lineColor);
		}

		public Indicators.VerticlaLineAtTime VerticlaLineAtTime(ISeries<double> input , DateTime lineTime, Brush lineColor)
		{
			return indicator.VerticlaLineAtTime(input, lineTime, lineColor);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.VerticlaLineAtTime VerticlaLineAtTime(DateTime lineTime, Brush lineColor)
		{
			return indicator.VerticlaLineAtTime(Input, lineTime, lineColor);
		}

		public Indicators.VerticlaLineAtTime VerticlaLineAtTime(ISeries<double> input , DateTime lineTime, Brush lineColor)
		{
			return indicator.VerticlaLineAtTime(input, lineTime, lineColor);
		}
	}
}

#endregion
