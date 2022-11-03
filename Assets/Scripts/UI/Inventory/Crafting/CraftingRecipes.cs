using System.Collections.Generic;

public static class CraftingRecipes
{
	public static Dictionary<HashSet<string>, string> RecipeList = new Dictionary<HashSet<string>, string>(HashSet<string>.CreateSetComparer())
	{
		// { ingredient 1, ingredient 2, ingredient 3 }, result

		{ new HashSet<string> { "RedPotion", "BluePotion" }, "PurplePotion" }
	};
}