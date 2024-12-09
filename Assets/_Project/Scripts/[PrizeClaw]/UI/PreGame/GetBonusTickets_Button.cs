using VG;


public class GetBonusTickets_Button : ButtonHandler
{
    
    protected override void OnClick()
    {
        Ads.Rewarded.Show(Key_Ad.prize_claw_ticket, onShown: (result) =>
        {
            if (result == Ads.Rewarded.Result.Success)
            {
                Saves.Float[Key_Save.prize_claw_tickets].Value += 1f;
            }

        });
    }
    
}