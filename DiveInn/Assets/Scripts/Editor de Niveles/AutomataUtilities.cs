namespace AutomataUtilities{
    using System;
    using System.Collections;
    using System.Collections.Generic;
	using System.IO;
    // using ComputeShaderUtility;
    using UnityEngine;
    using UnityEngine.UIElements;

    public struct Neighborhood{

		public List<Vector2Int> coordinates;
		public Texture2D screenshot;
		

		// public Neighborhood(List<Vector2Int> coordinates, RenderTexture screenshot, string name){
		// 	this.coordinates=coordinates;
		// 	this.screenshot=screenshot;
		// 	this.name=name;
		// }
		

		public Neighborhood(List<Vector2Int> coordinates){
			this.coordinates=coordinates;
			this.screenshot=null;
			
		}

		public String PrintCoords(){
			

			return coordinates.ToString();;
		}


    };

	

	public class ComputeBufferManager : MonoBehaviour{
		public static List<ComputeBuffer> buffers = new List<ComputeBuffer>();

		public static ComputeBuffer CreateBuffer(int count, int stride)
		{
			ComputeBuffer buffer = new ComputeBuffer(count, stride);
			buffers.Add(buffer);

			
			return buffer;
		}

		public static ComputeBuffer CreateBufferConstant(int count, int stride, ComputeBufferType type){
			ComputeBuffer buffer = new ComputeBuffer(count, stride, type);
			buffers.Add(buffer);

			
			return buffer;
		}

		public static void DisposeBuffer(ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Dispose();
				buffers.Remove(buffer);
			}
		}

		void OnApplicationQuit()
		{
			DisposeAllBuffers();
		}

		public static void DisposeAllBuffers()
		{
			foreach (var buffer in buffers)
			{
				if (buffer != null)
				{
					buffer.Dispose();
				}
			}
			buffers.Clear();
		}
		public static void DisposeHalfOfBuffers(){
			for(int i=0; i<20; i++){
				DisposeBuffer(buffers[i]);

			}
		}
	}

	public static class AutomataHelper{

		public static Vector2Int[] listToArray(List<Vector2Int> list){
				Vector2Int[] arr = new Vector2Int[list.Count];


				for(int i=0; i<list.Count; i++){
					arr[i]=list[i];
				}

				return arr;
			}
		public static IEnumerator TakeScreenshot(int width, int height, Camera myCamera, Neighborhood nh){

			yield return new WaitForEndOfFrame();
			myCamera.targetTexture= RenderTexture.GetTemporary(width, height, 16);
			
				

			RenderTexture renderTexture=myCamera.targetTexture;
			Texture2D result= new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
			Rect rect       =  new Rect(0,0, renderTexture.width, renderTexture.height);
			result.ReadPixels(rect, 0,0);
			result.Apply();
			nh.screenshot=result;


			RenderTexture.ReleaseTemporary(renderTexture);
			myCamera.targetTexture=null;

				

			Debug.Log("Screenshot Taken");




		}
		public static void CopyToClipboard(string text)
    	{
        TextEditor te = new TextEditor
        {
            text = text
        };
        te.SelectAll();
        te.Copy();
    	}
		public static RenderTexture PictureToRenderTexture(Texture2D tex){

			RenderTexture renderTexture = new RenderTexture(tex.width, tex.height, 24);
			renderTexture.enableRandomWrite=true;
			renderTexture.filterMode=FilterMode.Point;
			renderTexture.Create();

				// Set the RenderTexture as the active render target
			RenderTexture activeRenderTexture = RenderTexture.active;
			RenderTexture.active = renderTexture;

				// Copy the source texture into the RenderTexture using Graphics.Blit
			Graphics.Blit(tex, renderTexture);

			// Restore the active render texture
			RenderTexture.active = activeRenderTexture;

			return renderTexture;
		}
		public static Texture2D ConvertRenderTextureToTexture2D(RenderTexture rt)
    	{
        // Make the RenderTexture active
			RenderTexture.active = rt;

			// Create a new Texture2D with the same dimensions as the RenderTexture
			Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);

			// Read the pixels from the RenderTexture into the Texture2D
			texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
			texture.Apply();

			// Reset the active RenderTexture
			RenderTexture.active = null;

			return texture;
    	}

		public static void SaveRender(RenderTexture screen)
    	{
			 // Step 1: Convert RenderTexture to Texture2D
			Texture2D texture = AutomataHelper.ConvertRenderTextureToTexture2D(screen);

			// Step 2: Encode to PNG
			byte[] bytes = texture.EncodeToPNG();

			// Step 3: Save the PNG to a file
			string path = Application.dataPath + "/Corales"; // Save in Assets/Corales
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			// Step 4: Find the next available file number
			string[] existingFiles = Directory.GetFiles(path, "Coral.*.png");
			int nextNumber = 0;

			foreach (string file in existingFiles)
			{
				string fileName = Path.GetFileNameWithoutExtension(file);
				string[] splitName = fileName.Split('.');
				if (splitName.Length == 2 && int.TryParse(splitName[1], out int number))
				{
					nextNumber = Mathf.Max(nextNumber, number + 1);
				}
			}

			// Create the new file name with the incremented number
			string filePath = Path.Combine(path, $"Coral.{nextNumber}.png");
			File.WriteAllBytes(filePath, bytes);
			Debug.Log("Saved render texture to " + filePath);

			// Step 5: Create a Sprite from the Texture2D (optional)
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    	}


		public static double Norm(Vector2Int vec){
			double x= (double) vec.x;
        	double y= (double) vec.y;

        	return Math.Sqrt(x*x+y*y);
		}

		public static double Norm(Vector2 vec){
			double x= (double) vec.x;
        	double y= (double) vec.y;


        	return Math.Sqrt(x*x+y*y);

			
		}
		public static Vector2Int[] GenerateRingCoords(int radius, Vector2Int[] ring) {
			int auxPoint = 1 - radius;
			int count = 0;

			if (radius == 0) {
				ring[0] = new Vector2Int(0, 0);
				ring[1] = new Vector2Int(73, 0);
				return ring;
			}

			Vector2Int coordinates = new Vector2Int(radius, 0);

			ring[count++] = coordinates;
			ring[count++] = new Vector2Int(-coordinates.x, 0);
			ring[count++] = new Vector2Int(0, coordinates.x);
			ring[count++] = new Vector2Int(0, -coordinates.x);

			while (coordinates.x >= coordinates.y) {
				coordinates.y++;

				// Mid point inside or on the perimeter
				if (auxPoint <= 0) {
					auxPoint = auxPoint + 2 * coordinates.y + 1;
				} else {
					coordinates.x--;
					auxPoint = auxPoint + 2 * coordinates.y - 2 * coordinates.x + 1;
				}

				if (coordinates.x < coordinates.y) {
					break; // Correct early return by breaking the loop
				}

				// Use new instances of Vector2Int for each coordinate
				ring[count++] = new Vector2Int(coordinates.x, coordinates.y);
				ring[count++] = new Vector2Int(-coordinates.x, coordinates.y);
				ring[count++] = new Vector2Int(coordinates.x, -coordinates.y);
				ring[count++] = new Vector2Int(-coordinates.x, -coordinates.y);

				if (coordinates.x != coordinates.y) {
					ring[count++] = new Vector2Int(coordinates.y, coordinates.x);
					ring[count++] = new Vector2Int(-coordinates.y, coordinates.x);
					ring[count++] = new Vector2Int(coordinates.y, -coordinates.x);
					ring[count++] = new Vector2Int(-coordinates.y, -coordinates.x);
				}
			}

			// Add a marker at the end if needed
			if (count < ring.Length) {
				ring[count] = new Vector2Int(73, 0);
			}

			return ring;
		}
		public static Vector2Int[] GetFlatRingMatrix(){
			Vector2Int[][]ringMatrix= new Vector2Int[17][];

			Vector2Int[] ringArray=new Vector2Int[93*17];

			
			for(int i=0; i<17; i++){
				
				ringMatrix[i]=new Vector2Int[93];
				AutomataHelper.GenerateRingCoords(i, ringMatrix[i]);


			}

			for(int n=0;  n<17; n++){
				for(int m=0; m<93; m++){
					ringArray[(n*93)+m]=ringMatrix[n][m];
					
				}

			}

			return ringArray;
       
		}


	} 
	
	
	

    
}