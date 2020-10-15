namespace CalculatorApp.Models
{
    using System.ComponentModel;

    /// <summary>
    /// 시간단위 입니다.
    /// </summary>
    public enum TimeUnit
    {
        [Description("마이크로초")]
        Microsecond,

        [Description("밀리초")]
        Millisecond,

        [Description("초")]
        Second,

        [Description("분")]
        Minute,

        [Description("시간")]
        Hour,

        [Description("일")]
        Day,

        [Description("주")]
        Week,

        [Description("년")]
        Year
    }
}
