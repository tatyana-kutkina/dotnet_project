using System;
using System.Collections.Generic;
using System.IO;

namespace dotnet_project
{
	public class program
	{

		internal int sum;
		internal int n;
		internal int[] nominals;
		internal List<List<int>> combinations;

		public program(int sum, int[] nominals)
        {
			this.sum = sum;
			this.nominals = nominals;
			this.n = nominals.Length;
        }

		static int GetSum()
		{
			int sum = 0;
			string enter_sum = "";
			try
			{
				Console.WriteLine("Enter sum");
				enter_sum = Console.ReadLine();
				sum = Int32.Parse(enter_sum);

			}
			catch
			{
				Console.WriteLine("Incorrect format of data input.\n ");
			}

			if (sum < 0)
			{
				throw new Exception("Input value is negative");
			}
			return sum;
		}

		static int[] GetNominals()
		{
			Console.WriteLine("Enter nominals");
			List<string> string_nominals = new List<string>();
			string input = "";

			while (input != "0")
			{
				input = Console.ReadLine();
				if (input != "0")
				{
					string_nominals.Add(input);
				}
			}
			List<int> nominals = new List<int>();
			foreach (var nominal in string_nominals)
			{
                try
                {
					int val = Int32.Parse(nominal);
					if (nominals.Contains(val))
					{
						Console.WriteLine("Incorrect banknote value entered: it is a duplicate nominal, it will be excluded");

					}
					else if (val <= 0)
					{
						Console.WriteLine("Incorrect banknote value entered: it is negative ir zero, it will be excluded");

					}
					else
					{
						nominals.Add(val);
					}
				}
                catch
                {
					Console.WriteLine("Incorrect format of data input.\n ");
				}

			}
			return nominals.ToArray();
		}

		public void GetCombinations()
        {
			combinations = new List<List<int>>();
			GetCombinations(new int[n], sum, 0);
			if (combinations.Count == 0)
			{
				Console.WriteLine("It is impossible to exchange this amount with the presented bills");
			}
		}

		public void GetCombinations(int[] counts, int amount, int index)
        {
			if (amount == 0)
            {
				List<int> list = new List<int>();
				for (int i = 0; i < n; i++)
				{
					for (int j = 0; j < counts[i]; j++)
					{
						list.Add(nominals[i]);
					}
				}
				combinations.Add(list);
			}
			else if (amount > 0)
            {
				GetCombinationsLoop(counts, amount, index);
			}
		}

		public void GetCombinationsLoop(int[] counts, int amount, int index)
        {
			counts[index]++;
			GetCombinations(counts, amount - nominals[index], index);
			counts[index]--;
			if (index + 1 < n && nominals[index + 1] <= amount)
			{
				GetCombinationsLoop(counts, amount, index + 1);
			}
		}

		static void Main(string[] args)
		{
			int sum = GetSum();
			int[] nominals = GetNominals();

			program cash_mach = new program(sum, nominals);
			cash_mach.GetCombinations();

			int i = 1;
			foreach (List<int> x in cash_mach.combinations)
			{
				Console.WriteLine("combination number " + i);
				foreach(int val in x)
                {
					Console.WriteLine(val);
                }
				i++;
				Console.WriteLine("\n");

						
			}
		}

	}

}
