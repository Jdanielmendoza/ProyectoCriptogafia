using CryptoClassic.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoClassic.Core.Attacks
{
    /// <summary>
    /// Ejecuta el Ataque de Kasiski sobre un texto cifrado Vigenère y
    /// devuelve un informe con longitudes de clave probables.
    /// Implementa ICipher sólo para encajar en la GUI (Encrypt = analizar).
    /// </summary>
    public class KasiskiAttack : ICipher
    {
        public string Name => "Ataque de Kasiski (análisis)";

        // 'key' y 'Decrypt' no se usan, pero la interfaz los requiere
        public string Encrypt(string ciphertext, object key = null) =>
            Analyse(ciphertext);

        public string Decrypt(string ciphertext, object key = null) =>
            Analyse(ciphertext);

        private string Analyse(string cipher)
        {
            string clean = Regex.Replace(cipher.ToUpper(), "[^A-Z]", "");
            if (clean.Length < 30) return "El texto es demasiado corto para Kasiski.";

            const int ngram = 3; // trigramas
            var positions = new Dictionary<string, List<int>>();

            // 1️⃣ localizar repetidos
            for (int i = 0; i <= clean.Length - ngram; i++)
            {
                string sub = clean.Substring(i, ngram);
                if (!positions.ContainsKey(sub))
                    positions[sub] = new List<int>();
                positions[sub].Add(i);
            }

            var distances = new List<int>();
            foreach (var kv in positions.Where(p => p.Value.Count > 1))
            {
                var posList = kv.Value;
                for (int i = 0; i < posList.Count - 1; i++)
                    for (int j = i + 1; j < posList.Count; j++)
                        distances.Add(posList[j] - posList[i]);
            }

            if (distances.Count == 0)
                return "No se encontraron trigramas repetidos suficientes.";

            // 2️⃣ conteo de factores
            var factorFreq = new Dictionary<int, int>();
            foreach (int d in distances)
            {
                for (int f = 2; f <= 20; f++)          // buscamos claves 2-20
                    if (d % f == 0)
                        factorFreq[f] = factorFreq.GetValueOrDefault(f, 0) + 1;
            }

            var ordered = factorFreq
                          .OrderByDescending(kv => kv.Value)
                          .ThenBy(kv => kv.Key)
                          .Take(5)        // top-5
                          .ToList();

            // 3️⃣ informe legible
            var sb = new StringBuilder();
            sb.AppendLine("=== Informe Kasiski ===");
            sb.AppendLine($"Texto analizado: {clean.Length} letras");
            sb.AppendLine($"Repeticiones (trigramas): {positions.Count(p => p.Value.Count > 1)}");
            sb.AppendLine($"Total de distancias: {distances.Count}");
            sb.AppendLine();
            sb.AppendLine("Candidatos de longitud de clave (más frecuentes):");
            foreach (var (len, freq) in ordered)
                sb.AppendLine($"  • {len}  →  {freq} apariciones");

            if (ordered.Count > 0)
            {
                int best = ordered.First().Key;
                sb.AppendLine($"\n👉  **Se sugiere probar longitud = {best}**");
            }
            return sb.ToString();
        }
    }
}
