using VG2;


public class GetBonusTickets_Button : ButtonHandler
{
    
    protected override void OnClick()
    {
        Ads.Rewarded.Show(Key_Ad.prize_claw_ticket, onShown: (result) =>
        {
            if (result == Ads.Rewarded.Result.Success)
            {
                GameState.tickets.Value += 1f;
            }

        });
    }
    
}