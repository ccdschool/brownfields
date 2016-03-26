using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace wordwrap
{
	[TestFixture ()]
	public class WordWrap
	{
		[Test]
		public void testErzeugeZeilen() {
			var worte = new[]{ "1", "22", "333", "55555"};
			var lineLength = 5;
			var result = WordWrap.Erzeuge_Zeilen(worte, lineLength);
			Assert.AreEqual(new String[]{"1 22", "333", "55555"}, result);
		}

		[Test]
		public void testErzeugeZeilenMitZuLangemWort() {
			var worte = new[]{ "1", "22", "666666", "55555"};
			var lineLength = 5;
			var result = WordWrap.Erzeuge_Zeilen(worte, lineLength);
			Assert.AreEqual(new String[]{"1 22", "66666", "6", "55555"}, result);
		}


		static string[] Erzeuge_Zeilen(string[] worte, int maxLineLength) {
			List<string> lines = new List<string>();

			var i = 0;
			var line = "";
			while(true) {
				if	(i == worte.Length) {
					if	(line.Length != 0) {
						lines.Add(line);
					}						
					break;
				}

				String nextWord = worte[i];

				if (string.IsNullOrWhiteSpace(line) && nextWord.Length > maxLineLength) {
					String head = nextWord.Substring(0, maxLineLength);
					String body = nextWord.Substring(maxLineLength);
					lines.Add(head);
					worte[i] = body;
				}
				else {						
					int futureLength = line.Length + nextWord.Length + (line.Length > 0 ? 1 : 0);
					if (futureLength <= maxLineLength) {
						line += (line.Length > 0 ? " " : "") + nextWord;
						i++;
					}
					else {
						lines.Add(line);
						line = "";
					}
				}
			}				

			return lines.ToArray ();		
		}
	}
}