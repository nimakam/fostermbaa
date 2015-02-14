<?php
/**
 * The template for displaying the footer
 *
 * Contains footer content and the closing of the #main and #page div elements.
 *
 * @package WordPress
 * @subpackage Twenty_Fourteen
 * @since Twenty Fourteen 1.0
 */
?>

</div>
<!-- #main -->

<footer id="colophon" class="site-footer" role="contentinfo" style="height: 80px;">

  <?php get_sidebar( 'footer' ); ?>

  <div id="footer-wrapper" style="padding-top: 25px; font-size: 16px; margin-right: auto; margin-left: auto; max-width: 1102px; color: #fff;">
    <div style="float: left;">&#169 Copyright 2014 University of Washington</div>
    <div id="footer-links" style="float: right;">
      <a style="color: #fff;" href="mailto:mbaaeve@uw.edu">Contact Us</a>
    </div>
  </div>

  <!-- .site-info -->
</footer>
<!-- #colophon -->
</div>
<!-- #page -->

<?php wp_footer(); ?>
</body>
</html>