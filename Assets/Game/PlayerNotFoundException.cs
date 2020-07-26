using System;

public class PlayerNotFoundException : SystemException
{
    public PlayerNotFoundException(int playerId) : base(playerId + " does not exist")
    {
        
    }
}