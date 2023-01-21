﻿using System;
using System.Collections.Generic;
using System.Linq;
using DigiD.Common.EID.Models;
using DigiD.Common.EID.SessionModels;
using DigiD.Common.NFC.Enums;
using DigiD.Common.NFC.Helpers;

namespace DigiD.Common.EID.Helpers
{
    public static class CardHelper
    {
        private static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static Card GetCardByATR(byte[] atr)
        {
            if (atr == null)
                return null;

            Console.WriteLine($"ATR Card Scanned: {atr.ToHexString()}");

            var card = Card.AvailableCards.FirstOrDefault(x =>
            {
                Console.WriteLine($"Card: {x.ATR.ToHexString()}");

                if (EIDSession.IsDesktop)
                    return atr.ToHexString().Contains(x.ATR.ToHexString());

                if (x.ATR.Length == atr.Length)
                    return x.ATR.SequenceEqual(atr);

                return x.ATR.TakeLast(atr.Length + 1).Take(atr.Length).SequenceEqual(atr);
            });

            return card;
        }

        public static void SetCard(DocumentType documentType)
        {
            EIDSession.Card = Card.AvailableCards.FirstOrDefault(x => x.DocumentType == documentType);
        }
    }
}
