using System;
using Events.Handlers;
using Network;
using UnityEngine;
using EventHandler = Events.Handlers.EventHandler;

namespace Events.Parsers
{
    public class ParserFactory
    {

        public static EventHandler Parse(Packet packet)
        {
            var type = packet.Payload[0];

            if (type == 10)
            {
                PositionEvent pe = PositionParser.Parse(packet.Payload, 1);
                return new PositionEventHandler(pe);
            }
            
            Debug.LogError("Packet type not recognized " + type);
            throw new NotImplementedException();
        }
        
    }
}