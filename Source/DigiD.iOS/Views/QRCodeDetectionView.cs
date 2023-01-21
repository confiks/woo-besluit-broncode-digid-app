using System;
using System.Linq;
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace DigiD.iOS.Views
{
    [Register("QRCodeDetectionView")]
    public class QRCodeDetectionView : UIView
    {
        private AVCaptureVideoPreviewLayer _previewLayer;
        private AVCaptureSession _session;
        private MetadataObjectsDelegate _metadataObjectsDelegate;

        public Action<string> ObjectCaptured { get; set; }
        
        public QRCodeDetectionView()
        {
            Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _metadataObjectsDelegate?.Dispose();
            _previewLayer?.Dispose();
            _session?.Dispose();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            if (_previewLayer != null)
            {
                _previewLayer.Frame = new CGRect(0, 0, rect.Width, rect.Height);
            }
        }

        public override void LayoutSublayersOfLayer(CALayer layer)
        {
            base.LayoutSublayersOfLayer(layer);
            if (_previewLayer != null)
            {
                _previewLayer.Frame = Bounds;
            }
        }


        private void Initialize()
        {
            this.BackgroundColor = UIColor.Black;
            GetCameraPreview();
        }

        private void GetCameraPreview()
        {
            // Configure the AV input device
            var camera = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);

            if (camera == null)
                return;

            if (camera.LockForConfiguration(out _))
            {
                var ranges = camera.ActiveFormat.VideoSupportedFrameRateRanges;
                if (ranges.Length > 0)
                    camera.ActiveVideoMinFrameDuration = ranges[0].MinFrameDuration;
                camera.UnlockForConfiguration();
            }

            var cameraInput = AVCaptureDeviceInput.FromDevice(camera);

            // Create a capture session
            _session = new AVCaptureSession
            {
                SessionPreset = AVCaptureSession.PresetPhoto
            };

            // Set the camera as input device
            _session.AddInput(cameraInput);

            var metadataOutput = new AVCaptureMetadataOutput();
            if (_session.CanAddOutput(metadataOutput))
            {
                var metadataQueue = new DispatchQueue("com.AVCam.metadata");

                _metadataObjectsDelegate = new MetadataObjectsDelegate
                {
                    DidOutputMetadataObjectsAction = DidOutputMetadataObjects
                };

                metadataOutput.SetDelegate(_metadataObjectsDelegate, metadataQueue);

                _session.AddOutput(metadataOutput);
            }

            _previewLayer = new AVCaptureVideoPreviewLayer(_session)
            {
                Frame = Layer.Bounds, 
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill,
            };

            if (_previewLayer.Connection != null)
                _previewLayer.Connection.VideoOrientation = GetOrientation();
            
            Layer.AddSublayer(_previewLayer);

            metadataOutput.MetadataObjectTypes = AVMetadataObjectType.QRCode;
            _session.StartRunning();
        }

        private void DidOutputMetadataObjects(AVMetadataObject[] arg2)
        {
            if (arg2.Any())
            {
                _session.StopRunning();

                if (arg2.First() is AVMetadataMachineReadableCodeObject aVMetadataMachineReadableCodeObject)
                {
                    ObjectCaptured?.Invoke(aVMetadataMachineReadableCodeObject.StringValue);
                }
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var videoPreviewLayerConnection = _previewLayer?.Connection;

            if (videoPreviewLayerConnection != null)
            {
                videoPreviewLayerConnection.VideoOrientation = GetOrientation();
            }
        }

        private static AVCaptureVideoOrientation GetOrientation()
        {
            switch (UIApplication.SharedApplication.StatusBarOrientation)
            {
                case UIInterfaceOrientation.LandscapeLeft:
                    return AVCaptureVideoOrientation.LandscapeLeft;
                case UIInterfaceOrientation.LandscapeRight:
                    return AVCaptureVideoOrientation.LandscapeRight;
            }

            return AVCaptureVideoOrientation.Portrait;
        }
    }

    internal class MetadataObjectsDelegate : AVCaptureMetadataOutputObjectsDelegate
    {
        public Action<AVMetadataObject[]> DidOutputMetadataObjectsAction;

        public override void DidOutputMetadataObjects(AVCaptureMetadataOutput captureOutput, AVMetadataObject[] metadataObjects, AVCaptureConnection connection)
        {
            DidOutputMetadataObjectsAction?.Invoke(metadataObjects);
        }
    }
}
