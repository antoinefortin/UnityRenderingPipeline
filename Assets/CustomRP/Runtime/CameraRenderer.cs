using UnityEngine;
using UnityEngine.Rendering;

public class CameraRenderer
{

	ScriptableRenderContext context;

	Camera camera;
	const string bufferName = "Eat My Camera";

	CommandBuffer buffer = new CommandBuffer
	{
		name = bufferName
	};
	public void Render(ScriptableRenderContext context, Camera camera)
	{

		this.context = context;
		this.camera = camera;
		Setup();
		DrawVisibleGeometry();
		Submit();
	}

	void Setup()
	{
		buffer.ClearRenderTarget(true, true, Color.clear);
		buffer.BeginSample(bufferName);
		
		ExecuteBuffer();
		context.SetupCameraProperties(camera);
	}
	void DrawVisibleGeometry()
	{ 
		context.DrawSkybox(camera);
	}

	void Submit()
	{
		buffer.EndSample(bufferName);
		ExecuteBuffer();
		context.Submit();
	}

	void ExecuteBuffer()
	{
		context.ExecuteCommandBuffer(buffer);
		buffer.Clear();
	}
}