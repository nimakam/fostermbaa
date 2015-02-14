<?php
/**
 * Template Name: About Page
 *
 * @package WordPress
 * @subpackage Twenty_Fourteen
 * @since Twenty Fourteen 1.0
 */

get_header(); ?>

<div id="main-content" class="main-content">

  <div id="about-title" style="margin-right: auto; margin-left: auto; max-width: 1102px;">
    <div id="about-title-first-column" style="width: 758px; float: left; display: inline;">
      <img class="alignnone size-medium wp-image-196" style="width: 384px; height: 100%; float: left; display: inline;" alt="SampleAbout" src="/wp-content/uploads/2014/09/About-Feature.jpg"></img>


        <div id="about-title" style="background: #d7d9d9; padding: 24px; padding-top: 32px; width: 326px; height: 232px; float: left; display: inline;">
          <div style="font-size: 24px; font-weight: 900;">ABOUT</div>
          <div style="color: #352455; font-size: 22px;">Welcome to the MBAA</div>
          <div style="margin-top: 10px; font-size: 15px">The Evening MBAA is a professional networking society dedicated to enhancing the Foster Evening MBA experience. The Evening MBAA is dedicated to providing networking  opportunities on and off campus with current students, alumni, professors, and professionals in the Puget Sound region.</div>
        </div>
      </div>

    <div id="about-title-second-column" style="width: 320px; padding-left: 24px; float: left; display: inline;">

      <?php include 'links.php';?>


        <?php include 'join-mbaa.php';?>

    </div>
  </div>
  


  <div id="primary" class="content-area">
		<div id="content" class="site-content" role="main">

      <div id="about-content" style="max-width: 1102px; margin-right: auto; margin-left: auto">
       
          <?php
				// Start the Loop.
				while ( have_posts() ) : the_post();

					// Include the page content template.
					get_template_part( 'content', 'page' );

				endwhile;
			?>
       
      </div>

      


		</div><!-- #content -->
	</div><!-- #primary -->
	<?php get_sidebar( 'content' ); ?>
</div><!-- #main-content -->

<?php
get_footer();
