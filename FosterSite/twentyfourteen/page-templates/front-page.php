<?php
/**
 * Template Name: Front Page
 *
 * @package WordPress
 * @subpackage Twenty_Fourteen
 * @since Twenty Fourteen 1.0
 */

get_header(); ?>

<div id="main-content" class="main-content">

<?php

	// Include the featured content template.
  get_template_part( 'featured-content' );
	
?>
  <div style="display: block; clear: both;"></div>
  <div id="primary" class="content-area">
		<div id="content" class="site-content" role="main">

      <div style="max-width: 1102px; margin-right: auto; margin-left: auto">
        <div id="first-column" style="float: left; display: inline; width: 354px;">
          <?php
				// Start the Loop.
				while ( have_posts() ) : the_post();

					// Include the page content template.
					get_template_part( 'content', 'page' );

				endwhile;
			?>
          <div style="margin-top: 20px; margin-bottom: 20px; background: #d7d9d9; padding-right: 24px; padding-left: 24px; padding-top: 24px; padding-bottom: 24px;">
            <h1 style="margin-top: 0px;">Student of the Month</h1>
            <img style="margin-bottom: 20px;" width="295" height="249" class="alignnone size-full wp-image-191" alt="SampleStudentOfTheMonth" src="/wp-content/uploads/2014/08/SampleStudentOfTheMonth.jpg"></img>
            <p style="margin-bottom: 0px;">
              The life of a Foster Evening MBA student isn't easy. We all make countless sacrifices to achieve our academic and professional goals, while balancing our personal lives and helping others. The Evening MBAA would like to recognize this dedication and highlight those that go above and beyond to demonstrate the Foster spirit on a daily basis. Know someone who fits the bill? Please nominate someone today!
            </p>
            <a id="student-of-the-month" href="/about/student-of-the-month/" rel="StudentOfTheMonth">
              <p style="padding-top: 20px; margin-bottom: 0px;">Learn More</p>
            </a>
          </div>
        </div>
        <div id="second-column" style="float: left; display: inline; width: 354px; margin-left:20px;">
          
          <?php include 'news.php';?>
          
          <div style="margin-top: 20px; padding-right: 24px; padding-left: 24px; padding-top: 24px; padding-bottom: 24px; background: #fff;">
            <h1 style="margin-top: 0px;">Message from Your President</h1>
            <p style="margin-bottom: 0px;">You might be a 1st year student, nervous and excited about the start of a new adventure.  Or you could be a 2nd year student, fresh off a relaxing summer but ready to get back in the swing of things and capture some new magic this year.  Then again, you might be in your 3rd year, ready to begin the stretch run towards graduation, amazed by how fast it all seemed to go by.  I hope you all had a fantastic summer.  From M’s Games, BBQs, lunches, chance meetings, ...</p>
            <a id="messages-from-your-president" href="/about/messages-from-your-president/" rel="messages-from-your-president">
              <p style="padding-top: 20px; margin-bottom: 0px;">Read More</p>
            </a>
          </div>
        </div>
        <div id="third-column" style="float: left; display: inline; width: 354px; margin-left:20px;">
          <?php include 'join-mbaa.php';?>
          <?php include 'events.php';?>
        </div>
      </div>

      


		</div><!-- #content -->
	</div><!-- #primary -->
	<?php get_sidebar( 'content' ); ?>
</div><!-- #main-content -->

<?php
get_footer();
