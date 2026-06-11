#import <UIKit/UIKit.h>

extern "C"
{
    void ShowSystemAlertPopup()
    {
        dispatch_async(dispatch_get_main_queue(), ^
        {
            //NSString* nsTitle = [NSString stringWithUTF8String:title];
            //NSString* nsMessage = [NSString stringWithUTF8String:message];
            NSString *nsTitle = @"Warning"; 
            NSString *nsMessage = @"Some important message";
            
            UIAlertController* alert = [UIAlertController alertControllerWithTitle:nsTitle message:nsMessage preferredStyle:UIAlertControllerStyleAlert];
            UIAlertAction* confirm = [UIAlertAction actionWithTitle:@"Confirm" style:UIAlertActionStyleDefault handler:^(UIAlertAction* action)
            {
                UnitySendMessage("PlatformService", "OnSystemAlertPopupResult", "1");
            }];
            
            UIAlertAction* cancel = [UIAlertAction actionWithTitle:@"Cancel" style:UIAlertActionStyleCancel handler:^(UIAlertAction* action)
            {
                UnitySendMessage("PlatformService", "OnSystemAlertPopupResult", "0");
            }];
            
            [alert addAction:confirm];
            [alert addAction:cancel];
            
            UIWindowScene *scene = (UIWindowScene *)UIApplication.sharedApplication.connectedScenes.anyObject;
            UIViewController *root = scene.keyWindow.rootViewController;
            
            [root presentViewController:alert animated:YES completion:nil];
        });
    }
}
