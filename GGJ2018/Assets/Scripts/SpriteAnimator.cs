using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour {
	
		[SerializeField] protected Sprite[] spriteData;
		[SerializeField] protected bool autoplay = false;
		[SerializeField] protected bool loop = false;
		[SerializeField] protected float animationDuration = 1.0f;
		[SerializeField] protected Image image;

		protected int currentFrame = 0;
		protected bool isPlaying = false;
		protected float timer = 0.0f;

		void Start()
		{
			if(this.spriteData == null || this.spriteData.Length == 0)
			{
				return;
			}

			if(this.image == null)
			{
				return;
			}

			this.image.sprite = this.spriteData[0];

			this.currentFrame = 0;
			this.isPlaying = false;
			this.timer = 0.0f;

			if(this.autoplay)
			{
				this.isPlaying = true;
			}
		}

		void Update()
		{
			if(this.spriteData == null || this.spriteData.Length == 0)
			{
				return;
			}

			if(this.image == null)
			{
				return;
			}

			if(this.isPlaying == false)
			{
				return;
			}

			float frameChangeTimer = this.animationDuration / this.spriteData.Length;
			this.timer += Time.deltaTime;
			if(this.timer >= frameChangeTimer)
			{
				this.timer = 0.0f;
				this.currentFrame++;

				if(this.currentFrame >= this.spriteData.Length)
				{
					if(this.loop)
					{
						this.currentFrame = 0;
					}
					else
					{
						this.isPlaying = false;
						this.currentFrame = this.spriteData.Length - 1;
					}
				}

				this.image.sprite = this.spriteData[this.currentFrame];
			}
		}
}
