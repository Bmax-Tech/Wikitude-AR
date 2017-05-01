//
//  WTRecognizedTarget.h
//  WikitudeNativeSDK
//
//  Created by Andreas Schacherbauer on 28/04/15.
//  Copyright (c) 2015 Wikitude. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "WTTarget.h"

NS_ASSUME_NONNULL_BEGIN


/**
 * WTImageTarget represents image targets that are found by an image tracker.
 */
@interface WTImageTarget : WTTarget

/**
 * @return name The name of the image target
 */
@property (nonatomic, strong, readonly) NSString                    *name;

/**
 * @brief The distance from the camera to the image target in millimeter.
 *
 * This value only contains reliable values if the .wtc file or the cloud archive included physical image target heights.
 *
 * @return distanceToTarget The physical distance in millimeter between the camera and the image target.
 */
@property (nonatomic, assign, readonly) unsigned int                distanceToTarget;

@end

NS_ASSUME_NONNULL_END
