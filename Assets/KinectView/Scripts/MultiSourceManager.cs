using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class MultiSourceManager : MonoBehaviour {
    public int ColorWidth { get; private set; }
    public int ColorHeight { get; private set; }
    
    private KinectSensor _Sensor;
    private MultiSourceFrameReader _Reader;
    private Texture2D _ColorTexture;
    private ushort[] _DepthData;
    private byte[] _ColorData;

    public Texture2D GetColorTexture()
    {
        return _ColorTexture;
    }
    
    public ushort[] GetDepthData()
    {
        return _DepthData;
    }

    void Start () 
    {
        _Sensor = KinectSensor.GetDefault();
        
        if (_Sensor != null) 
        {
            _Reader = _Sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth);
            
            var colorFrameDesc = _Sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);
            ColorWidth = colorFrameDesc.Width;
            ColorHeight = colorFrameDesc.Height;
            
            _ColorTexture = new Texture2D(colorFrameDesc.Width, colorFrameDesc.Height, TextureFormat.RGBA32, false);
            _ColorData = new byte[colorFrameDesc.BytesPerPixel * colorFrameDesc.LengthInPixels];
            
            var depthFrameDesc = _Sensor.DepthFrameSource.FrameDescription;
            _DepthData = new ushort[depthFrameDesc.LengthInPixels];
            
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }
    }
    
    void Update () 
    {
        if (_Reader != null) 
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                var colorFrame = frame.ColorFrameReference.AcquireFrame();
                if (colorFrame != null)
                {
                    var depthFrame = frame.DepthFrameReference.AcquireFrame();
                    if (depthFrame != null)
                    {
                        /*ColorFrameSource cds = _Sensor.ColorFrameSource;
                        for (int k = 0; k < cds.FrameDescription.Width; k++)
                        {
                            for(int i = 0; i< cds.FrameDescription.Height; i++)
                            {
                                _ColorData[k]
                            }
                        }*/
                        colorFrame.CopyConvertedFrameDataToArray(_ColorData, ColorImageFormat.Rgba);
                        for(int i = 0; i< _ColorData.Length; i+= (int)colorFrame.FrameDescription.BytesPerPixel)
                        {
                            if(_ColorData[i]< 0x46 && _ColorData[i+1] > 0xCD && _ColorData[i+2]< 0x46){
                                _ColorData[i] = 0xFF;
                                _ColorData[i] = 0xFF;
                                _ColorData[i] = 0xFF;
                            }
                            else
                            {
                                _ColorData[i] = 0x00;
                                _ColorData[i] = 0x00;
                                _ColorData[i] = 0x00;
                            }

                        }

                        _ColorTexture.LoadRawTextureData(_ColorData);
                        _ColorTexture.Apply();
                        
                        depthFrame.CopyFrameDataToArray(_DepthData);
                        
                        depthFrame.Dispose();
                        depthFrame = null;
                    }
                
                    colorFrame.Dispose();
                    colorFrame = null;
                }
                
                frame = null;
            }
        }
    }
    
    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }
        
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            
            _Sensor = null;
        }
    }
}
