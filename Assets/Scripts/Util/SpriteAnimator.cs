using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameRate; //Vitesse de l animation

    int currentFrame;
    float timer;

    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameRate = 0.16f)
    {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.frameRate = frameRate;
    }

    public void Start()
    {
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer > frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Count; //permet d'augmenter currentFrame ou de le repasser a 0 si on a fait le tour de l'animation
            spriteRenderer.sprite = frames[currentFrame];
            timer -= frameRate; //permet de repasser le timer a 0
        }
    }

    public List<Sprite> Frames
    {
        get { return frames; }
    }
}
