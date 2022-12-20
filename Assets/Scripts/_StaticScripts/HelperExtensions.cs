using System.Collections.Generic;
using UnityEngine;

public static class HelperExtensions
{
	public static Dictionary<string, float> GetAnimatorClipLengths(Animator animator)
	{
		var clips = animator.runtimeAnimatorController.animationClips;
		var clipLengths = new Dictionary<string, float>();

		foreach (var clip in clips)
		{
			clipLengths[clip.name] = clip.length;
		}

		return clipLengths;
	}
}