//
//  WTImageTrackerConfiguration.h
//  WikitudeNativeSDK
//
//  Created by Alexandru Florea on 24/10/16.
//  Copyright © 2016 Wikitude. All rights reserved.
//

#ifndef WTImageTrackerConfiguration_h
#define WTImageTrackerConfiguration_h

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN


/**
 * @brief WTImageTrackerConfiguration represents additional values that can be used to configure how an image tracker behaves.
 */
@interface WTImageTrackerConfiguration : NSObject

/**
 * @brief An array of NSString objects that represent which targets of the .wtc file should be treated as extended targets.
 *
 * @discussion The extended targets array is usually set through the client tracker creation factory method.
 */
@property (nonatomic, strong) NSArray               *extendedTargets;

@end

NS_ASSUME_NONNULL_END

#endif /* WTImageTrackerConfiguration_h */
