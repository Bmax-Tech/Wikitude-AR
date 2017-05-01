//
//  WTInstantTracker.h
//  WikitudeNativeSDK
//
//  Created by Alexandru Florea on 17/11/16.
//  Copyright © 2016 Wikitude. All rights reserved.
//

#ifndef WTInstantTracker_h
#define WTInstantTracker_h

#import <Foundation/Foundation.h>

#import "WTBaseTracker.h"

NS_ASSUME_NONNULL_BEGIN

/**
 * WTInstantTrackingState indicates the current state in which an instant tracker is.
 */
typedef NS_ENUM(NSInteger, WTInstantTrackingState) {
    /**
     * WTInstantTarckerInitializing indicates that the instant tracker is in initialization state, which allows the origin of the tracking scene to be set, as well as the device height above ground.
     */
    WTInstantTrackerInitializing = 0,
    /**
     * WTInstantTrackerTracking indicates that the instant tracker is in tracking state, which means that the current scene is being tracker and augmentations can be placed.
     */
    WTInstantTrackerTracking = 1
};

@class WTInstantTracker;
@class WTInitializationPose;
@class WTInstantTarget;


/**
 * @Brief WTInstantTrackerDelegate provides methods that propagate information about tracker state changes.
 *
 * Tracker objects are not created by calling there init method, but through the provided factory methods of WTTrackerManager.
 *
 * @discussion Matrix pointer that are provided inside the WTInstantTarget objects are only valid in the current method scope. If they are needed at a later point in time, they have to be copied into different memory that is controlled by the Wikitude Native SDK hosting application.
 */
@protocol WTInstantTrackerDelegate

/**
 * @Brief Called whenever a tracker changes its state internally as a result of calling setActiveInstantTrackingState: on it.
 *
 * @param instantTracker The tracker object whose state changed
 * @param newState The new state of the tracker
 */
- (void)instantTracker:(nonnull WTInstantTracker *)instantTracker didChangeState:(WTInstantTrackingState)newState;

/**
 * @brief Called whenever an initialization update is being sent by the instant tracker. The tracking data can be used to draw gravity a aligned plane or other augmentations while the tracker is in the initialization state.
 *
 * @param instantTracker The tracker object that sent the initialization update.
 * @param pose Information about the gravity aligned plane
 */
- (void)instantTracker:(nonnull WTInstantTracker *)instantTracker didChangeInitializationPose:(nonnull WTInitializationPose *)pose;

/**
 * @Brief Called whenever a tracker starts tracking the scene for the first time or after it was lost.
 *
 * @param instantTracker The tracker object that recognized the scene
 */
- (void)instantTrackerDidStartTracking:(nonnull WTInstantTracker *)instantTracker;

/**
 * @brief Called whenever the current scene is being tracked again.
 *
 * @param instantTracker The tracker object that tracked scene
 * @param target Information about the scene being tracked
 */
- (void)instantTracker:(nonnull WTInstantTracker *)instantTracker didTrack:(nonnull WTInstantTarget *)target;

/**
 * @brief Called whenever the current scene is lost
 *
 * @discussion All WTInstantTarget matrix pointer are set to nullptr and should not be used.
 *
 * @param instantTracker The tracker object that lost the scene.
 */
- (void)instantTrackerDidStopTracking:(nonnull WTInstantTracker *)instantTracker;

@end

/**
 * @brief Represents a tracker that can instantly start tracking a scene with any markers.
 * 
 * @discussion Instant trackers start in initializing state, during which the origin of the tracked scene can be set by rotating the device. Displaying a gravity aligned plane or target in the center of the screen can help users through this process. After this was set, the instant tracker can instantly transition to the tracking state, during which the actual tracking of the scene takes place.
 */
@interface WTInstantTracker : NSObject 

/**
 * @brief The delegate object that is associated with this instant tracker object.
 *
 * @discussion The delegate object is usually set through the appropriate WTTrackerManager factory method.
 */
@property (nonatomic, weak) id<WTInstantTrackerDelegate> delegate;

/**
 * @brief Changes the tracking state of the instant tracker
 *
 * @discussion The tracking state is not changed immediately, and the didChangeState method can be used to get notified of exactly when that happens.
 *
 * @param state The new state to which the instant tracker should switch to
 */
- (void)setActiveInstantTrackingState:(WTInstantTrackingState)state;

/**
 * @brief Allows changing the estimated height at which the device is currently above the ground.
 *
 * @discussion Setting this to an appropriate value will allow the augmentations to have a scale close to the one they would have in reality.
 *
 * @param height The estimated device height above the ground
 */
- (void)setDeviceHeightAboveGround:(NSNumber *)height;

@end

NS_ASSUME_NONNULL_END

#endif /* WTInstantTracker_h */
