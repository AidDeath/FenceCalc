namespace FenceCalc.Models
{
    public class SavedFence
    {
        public int FenceWidth { get; set; }
        public int SideSpaceWidth { get; set; }
        public int SpaceBetweenWoods { get; set; }
        public int WoodsCount { get; set; }

        public override string ToString()
        {
            return $"{FenceWidth}, {WoodsCount}шт, через {SpaceBetweenWoods}, {SideSpaceWidth} по бокам";
        }
    }
}