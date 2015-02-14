<?php
/**
 * The Header for our theme
 *
 * Displays all of the <head> section and everything up till <div id="main">
 *
 * @package WordPress
 * @subpackage Twenty_Fourteen
 * @since Twenty Fourteen 1.0
 */
?><!DOCTYPE html>
<!--[if IE 7]>
<html class="ie ie7" <?php language_attributes(); ?>>
<![endif]-->
<!--[if IE 8]>
<html class="ie ie8" <?php language_attributes(); ?>>
<![endif]-->
<!--[if !(IE 7) | !(IE 8) ]><!-->
<html <?php language_attributes(); ?>>
<!--<![endif]-->
<head>
	<meta charset="<?php bloginfo( 'charset' ); ?>">
	<meta name="viewport" content="width=device-width">
	<title><?php wp_title( '|', true, 'right' ); ?></title>
	<link rel="profile" href="http://gmpg.org/xfn/11">
	<link rel="pingback" href="<?php bloginfo( 'pingback_url' ); ?>">
	<!--[if lt IE 9]>
	<script src="<?php echo get_template_directory_uri(); ?>/js/html5.js"></script>
	<![endif]-->
	<?php wp_head(); ?>
</head>

<body <?php body_class(); ?>>
<div id="page" class="hfeed site">
	<?php if ( get_header_image() ) : ?>
	<div id="site-header">
		<a href="<?php echo esc_url( home_url( '/' ) ); ?>" rel="home">
			<img src="<?php header_image(); ?>" width="<?php echo get_custom_header()->width; ?>" height="<?php echo get_custom_header()->height; ?>" alt="">
		</a>
		<div id="social-wrapper" class="social-wrapper" style="float: right; opacity: 0.5; padding-top: 18px;">
			<div id="social-icon-wrapper" class="social-icon-wrapper"">
				<div id="social-icon" class="social-icon" style="float: left; padding-left: 10px;">
					<a href="mailto:mbaaeve@uw.edu">
						<img alt="help" src="http://www.fostermbaa.com/wp-content/themes/twentyfourteen/images/help.png">
					</a>	
				</div>				
				<div id="social-icon" class="social-icon" style="float: left; padding-left: 10px;">
					<a href="https://www.facebook.com/groups/evemba2013/">
						<img alt="facebook" src="http://www.fostermbaa.com/wp-content/themes/twentyfourteen/images/facebook.png">
					</a>	
				</div>				
				<div id="social-icon" class="social-icon" style="float: left; padding-left: 10px;">
					<a href=" https://twitter.com/FosterEVEMBA">
						<img alt="twitter" src="http://www.fostermbaa.com/wp-content/themes/twentyfourteen/images/twitter.png">
					</a>	
				</div>
				<div id="social-icon" class="social-icon" style="float: left; padding-left: 10px;">
					<a href="https://www.linkedin.com/edu/school?id=19660">
						<img alt="linkedin" src="http://www.fostermbaa.com/wp-content/themes/twentyfourteen/images/linkedin.png">
					</a>	
				</div>
				<div id="social-icon" class="social-icon" style="float: left; padding-left: 10px;">
					<a href="https://www.youtube.com/playlist?list=PLfZAEtrz8B8tBkM4eZjN7hmjo2NAYsC6I">
						<img alt="you-tube" src="http://www.fostermbaa.com/wp-content/themes/twentyfourteen/images/you-tube.png">
					</a>	
				</div>	
			</div>	
		</div>
	</div>
	<?php endif; ?>

	<header id="masthead" class="site-header" role="banner">
		<div class="header-main">
			<h1 class="site-title"><a href="<?php echo esc_url( home_url( '/' ) ); ?>" rel="home"><?php bloginfo( 'name' ); ?></a></h1>

			<div class="search-toggle">
				<a href="#search-container" class="screen-reader-text"><?php _e( 'Search', 'twentyfourteen' ); ?></a>
			</div>

			<nav id="primary-navigation" class="site-navigation primary-navigation" role="navigation">
				<button class="menu-toggle"><?php _e( 'Primary Menu', 'twentyfourteen' ); ?></button>
				<a class="screen-reader-text skip-link" href="#content"><?php _e( 'Skip to content', 'twentyfourteen' ); ?></a>
				<?php wp_nav_menu( array( 'theme_location' => 'primary', 'menu_class' => 'nav-menu' ) ); ?>
			</nav>
		</div>

		<div id="search-container" class="search-box-wrapper hide">
			<div class="search-box">
				<?php get_search_form(); ?>
			</div>
		</div>
	</header><!-- #masthead -->

	<div id="main" class="site-main">
