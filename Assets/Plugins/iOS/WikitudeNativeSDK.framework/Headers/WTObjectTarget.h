//
//  WTObjectTarget.h
//  WikitudeNativeSDK
//
//  Created by Alexandru Florea on 03/11/16.
//  Copyright © 2016 Wikitude. All rights reserved.
//

#ifndef WTObjectTarget_h
#define WTObjectTarget_h

#import <Foundation/Foundation.h>

#import "WTTarget.h"

NS_ASSUME_NONNULL_BEGIN

/**
 * WTObjectTarget represents image targets that are found by an object tracker.
 */
@interface WTObjectTarget : WTTarget

/**
 * @return name The name of the object target
 */
@property (nonatomic, strong, readonly) NSString                    *name;

/**
 * @brief The distance from the camera to the object target in millimeter.
 *
 * @return distanceToTarget The physical distance in millimeter between the camera and the object target.
 */
@property (nonatomic, assign, readonly) unsigned int                distanceToTarget;

@end

NS_ASSUME_NONNULL_END


#endif /* WTObjectTarget_h */
